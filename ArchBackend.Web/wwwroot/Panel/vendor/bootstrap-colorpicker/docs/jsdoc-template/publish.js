/*global env: true */
"use strict"

var doop = require("jsdoc/util/doop")
var fs = require("jsdoc/fs")
var helper = require("jsdoc/util/templateHelper")
var logger = require("jsdoc/util/logger")
var path = require("jsdoc/path")
var taffy = require("taffydb").taffy
var template = require("jsdoc/template")
var util = require("util")

var htmlsafe = helper.htmlsafe
var linkto = helper.linkto
var resolveAuthorLinks = helper.resolveAuthorLinks
var scopeToPunc = helper.scopeToPunc
var hasOwnProp = Object.prototype.hasOwnProperty

var data
var view

var outdir = path.normalize(env.opts.destination)

function find(spec) {
  return helper.find(data, spec)
}

function tutoriallink(tutorial) {
  return helper.toTutorial(tutorial, null, {
    tag: "em",
    classname: "disabled",
    prefix: "Tutorial: "
  })
}

function getAncestorLinks(doclet) {
  return helper.getAncestorLinks(data, doclet)
}

function hashToLink(doclet, hash) {
  if (!/^(#.+)/.test(hash)) {
    return hash
  }

  var url = helper.createLink(doclet)

  url = url.replace(/(#.+|$)/, hash)
  return '<a href="' + url + '">' + hash + "</a>"
}

function needsSignature(doclet) {
  var needsSig = false

  // function and class definitions always get a signature
  if (doclet.kind === "function" || doclet.kind === "class") {
    needsSig = true
  } else if (
    doclet.kind === "typedef" &&
    doclet.type &&
    doclet.type.names &&
    doclet.type.names.length
  ) {
    // typedefs that contain functions get a signature, too
    for (var i = 0, l = doclet.type.names.length; i < l; i++) {
      if (doclet.type.names[i].toLowerCase() === "function") {
        needsSig = true
        break
      }
    }
  }

  return needsSig
}

function getSignatureAttributes(item) {
  var attributes = []

  if (item.optional) {
    attributes.push("opt")
  }

  if (item.nullable === true) {
    attributes.push("nullable")
  } else if (item.nullable === false) {
    attributes.push("non-null")
  }

  return attributes
}

function updateItemName(item) {
  var attributes = getSignatureAttributes(item)
  var itemName = item.name || ""

  if (item.variable) {
    itemName = "&hellip;" + itemName
  }

  if (attributes && attributes.length) {
    itemName = util.format(
      '%s<span class="signature-attributes">%s</span>',
      itemName,
      attributes.join(", ")
    )
  }

  return itemName
}

function addParamAttributes(params) {
  return params
    .filter(function (param) {
      return param.name && param.name.indexOf(".") === -1
    })
    .map(updateItemName)
}

function buildItemTypeStrings(item) {
  var types = []

  if (item && item.type && item.type.names) {
    item.type.names.forEach(function (name) {
      types.push(linkto(name, htmlsafe(name)))
    })
  }

  return types
}

function buildAttribsString(attribs) {
  var attribsString = ""

  if (attribs && attribs.length) {
    attribsString = htmlsafe(util.format("(%s) ", attribs.join(", ")))
  }

  return attribsString
}

function addNonParamAttributes(items) {
  var types = []

  items.forEach(function (item) {
    types = types.concat(buildItemTypeStrings(item))
  })

  return types
}

function addSignatureParams(f) {
  var params = f.params ? addParamAttributes(f.params) : []
  f.signature = util.format("%s(%s)", f.signature || "", params.join(", "))
}

function addSignatureReturns(f) {
  var attribs = []
  var attribsString = ""
  var returnTypes = []
  var returnTypesString = ""

  // jam all the return-type attributes into an array. this could create odd results (for example,
  // if there are both nullable and non-nullable return types), but let's assume that most people
  // who use multiple @return tags aren't using Closure Compiler type annotations, and vice-versa.
  if (f.returns) {
    f.returns.forEach(function (item) {
      helper.getAttribs(item).forEach(function (attrib) {
        if (attribs.indexOf(attrib) === -1) {
          attribs.push(attrib)
        }
      })
    })

    attribsString = buildAttribsString(attribs)
  }

  if (f.returns) {
    returnTypes = addNonParamAttributes(f.returns)
  }
  if (returnTypes.length) {
    returnTypesString = util.format(
      " &rarr; %s{%s}",
      attribsString,
      returnTypes.join("|")
    )
  }

  f.signature =
    '<span class="signature">' +
    (f.signature || "") +
    "</span>" +
    '<span class="type-signature">' +
    returnTypesString +
    "</span>"
}

function addSignatureTypes(f) {
  var types = f.type ? buildItemTypeStrings(f) : []

  f.signature =
    (f.signature || "") +
    '<span class="type-signature">' +
    (types.length ? " :" + types.join("|") : "") +
    "</span>"
}

function addAttribs(f) {
  var attribs = helper.getAttribs(f)
  var attribsString = buildAttribsString(attribs)

  f.attribs = util.format(
    '<span class="type-signature">%s</span>',
    attribsString
  )
}

function shortenPaths(files, commonPrefix) {
  Object.keys(files).forEach(function (file) {
    files[file].shortened = files[file].resolved
      .replace(commonPrefix, "")
      // always use forward slashes
      .replace(/\\/g, "/")
  })

  return files
}

function getPathFromDoclet(doclet) {
  if (!doclet.meta) {
    return null
  }

  return doclet.meta.path && doclet.meta.path !== "null"
    ? path.join(doclet.meta.path, doclet.meta.filename)
    : doclet.meta.filename
}

function generate(type, title, docs, filename, resolveLinks) {
  resolveLinks = resolveLinks === false ? false : true

  var docData = {
    type: type,
    title: title,
    docs: docs
  }

  var outpath = path.join(outdir, filename),
    html = view.render("container.tmpl", docData)

  if (resolveLinks) {
    html = helper.resolveLinks(html) // turn {@link foo} into <a href="foodoc.html">foo</a>
  }

  fs.writeFileSync(outpath, html, "utf8")
}

function generateSourceFiles(sourceFiles, encoding) {
  encoding = encoding || "utf8"
  Object.keys(sourceFiles).forEach(function (file) {
    var source
    // links are keyed to the shortened path in each doclet's `meta.shortpath` property
    var sourceOutfile = helper.getUniqueFilename(sourceFiles[file].shortened)
    helper.registerLink(sourceFiles[file].shortened, sourceOutfile)

    try {
      source = {
        kind: "source",
        code: helper.htmlsafe(
          fs.readFileSync(sourceFiles[file].resolved, encoding)
        )
      }
    } catch (e) {
      logger.error("Error while generating source file %s: %s", file, e.message)
    }

    generate(
      "Source",
      sourceFiles[file].shortened,
      [source],
      sourceOutfile,
      false
    )
  })
}

/**
 * Look for classes or functions with the same name as modules (which indicates that the module
 * exports only that class or function), then attach the classes or functions to the `module`
 * property of the appropriate module doclets. The name of each class or function is also updated
 * for display purposes. This function mutates the original arrays.
 *
 * @private
 * @param {Array.<module:jsdoc/doclet.Doclet>} doclets - The array of classes and functions to
 * check.
 * @param {Array.<module:jsdoc/doclet.Doclet>} modules - The array of module doclets to search.
 */
function attachModuleSymbols(doclets, modules) {
  var symbols = {}

  // build a lookup table
  doclets.forEach(function (symbol) {
    symbols[symbol.longname] = symbols[symbol.longname] || []
    symbols[symbol.longname].push(symbol)
  })

  return modules.map(function (module) {
    if (symbols[module.longname]) {
      module.modules = symbols[module.longname]
      // Only show symbols that have a description. Make an exception for classes, because
      // we want to show the constructor-signature heading no matter what.
        .filter(function (symbol) {
          return symbol.description || symbol.kind === "class"
        })
        .map(function (symbol) {
          symbol = doop(symbol)

          if (symbol.kind === "class" || symbol.kind === "function") {
            symbol.name = symbol.name.replace("module:", '(require("') + '"))'
          }

          return symbol
        })
    }
  })
}

/**
 * Create the navigation sidebar.
 * @param {object} members The members that will be used to create the sidebar.
 * @param {array<object>} members.classes
 * @param {array<object>} members.externals
 * @param {array<object>} members.globals
 * @param {array<object>} members.mixins
 * @param {array<object>} members.modules
 * @param {array<object>} members.namespaces
 * @param {array<object>} members.tutorials
 * @param {array<object>} members.events
 * @param {array<object>} members.interfaces
 * @return {array} The HTML for the navigation sidebar.
 */
function buildNav(members) {
  var nav = []
  var seen = {}
  var seenTutorials = {}
  var conf = env.conf.templates || {}
  conf.default = conf.default || {}
  var tutorialsTitle = conf.default.tutorialsTitle || "Tutorials"

  nav.push(buildNavLink('logo', '<a class="nav-logo" href="index.html" title="Bootstrap Colorpicker">' +
    '<img alt="Home" src="logo.svg" /></a>' +
    '<span class="nav-logo-title">Bootstrap Colorpicker</span>'))

  // add searchBox
  if (env.conf.templates.default.searchBox && (env.conf.templates.default.searchBox.enabled !== false)) {
    nav.push(`<li class="search-box"><input placeholder="Search" class="form-control" type="search" /></li>`)
  }

  nav = nav.concat(buildMemberNav(members.tutorials, tutorialsTitle, seenTutorials, linktoTutorial))

  nav = nav.concat(buildMemberNav(members.classes, "Classes", seen, linkto))
  nav = nav.concat(buildMemberNav(members.modules, "Modules", {}, linkto))
  nav = nav.concat(buildMemberNav(members.externals, "Externals", seen, linktoExternal))
  nav = nav.concat(buildMemberNav(members.events, "Events", seen, linkto))
  nav = nav.concat(buildMemberNav(members.namespaces, "Namespaces", seen, linkto))
  nav = nav.concat(buildMemberNav(members.mixins, "Mixins", seen, linkto))
  nav = nav.concat(buildMemberNav(members.interfaces, "Interfaces", seen, linkto))

  if (members.globals.length) {
    nav.push(buildNavHeading(linkto('global', 'Globals')))

    members.globals.forEach(function (item) {
      if (item.kind !== "typedef" && !hasOwnProp.call(seen, item.longname)) {
        nav.push(buildNavItem(buildNavType(item.kind, linkto(item.longname, item.name)), item.kind))
      }

      seen[item.longname] = true
    })
  }

  return nav.join('')
}

function buildMemberNav(items, itemHeading, itemsSeen, linktoFn) {
  var nav = []
  var conf = env.conf.templates || {}
  conf.default = conf.default || {}
  var tutorialsTitle = conf.default.tutorialsTitle || "Tutorials"

  conf.default = conf.default || {}

  if (items && items.length) {
    var itemsNav = ""

    nav.push(buildNavHeading(itemHeading))

    items.forEach(function (item) {
      var methods = find({kind: "function", memberof: item.longname})
      var members = find({kind: "member", memberof: item.longname})
      var displayName

      if (!hasOwnProp.call(item, "longname")) {
        nav.push(buildNavItem(linkfoFn('', item.name), item.kind))
        return
      }

      if (!hasOwnProp.call(itemsSeen, item.longname)) {
        if (!!conf.default.useLongnameInNav) {
          displayName = item.longname

          if (conf.default.useLongnameInNav > 0 && conf.default.useLongnameInNav !== true) {
            var num = conf.default.useLongnameInNav
            var cropped = item.longname.split(".").slice(-num).join(".")
            if (cropped !== displayName) {
              displayName = "..." + cropped
            }
          }
        } else {
          displayName = item.name
        }

        displayName = displayName.replace(/^module:/g, "")

        if (itemHeading === tutorialsTitle) {
          nav.push(buildNavItem(linktoFn(item.longname, displayName), item.kind))
        } else {
          nav.push(buildNavHeading(buildNavType(item.kind, linktoFn(item.longname, displayName))))
        }

        if (methods.length) {
          methods.forEach(function (method) {
            if (method.inherited && conf.showInheritedInNav === false) {
              return
            }

            nav.push(buildNavItem(buildNavType(method.kind, linkto(method.longname, method.name)), method.kind))
          })
        }

        itemsSeen[item.longname] = true
      }
    })
  }

  nav.push();

  return nav
}

function linktoTutorial(longName, name) {
  return tutoriallink(name)
}

function linktoExternal(longName, name) {
  return linkto(longName, name.replace(/(^"|"$)/g, ""))
}

/**
 * Helper to generate navigation list link wrapper around navigation links for
 * locations.
 *
 * @param {String} linkClass navigation link classname
 * @param {String} linkContent navigation link HTML content
 * @return {String}
 */
function buildNavLink(linkClass, linkContent) {
  return [
    '<li class="nav-link nav-' + linkClass + '-link">',
    linkContent,
    '</li>'
  ].join('')
}

/**
 * Helper to generate navigation list header wrapper around navigation header content
 * for headings and filenames.
 *
 * @param {String} content navigation header content
 * @return {String}
 */
function buildNavHeading(content) {
  return [
    '<li class="nav-heading">',
    content,
    '</li>'
  ].join('')
}

/**
 * Helper for generating generic navigation wrapper around content passed for
 * methods, and types.
 *
 * @param {String} itemContent navigation item content
 * @param {String} itemKind
 * @return {String}
 */
function buildNavItem(itemContent, itemKind) {
  itemKind = itemKind || 'default';
  return [
    `<li class="nav-item nav-item-kind-${itemKind}">`,
    itemContent,
    '</li>'
  ].join('')
}

function buildNavType(type, typeLink) {
  return [
    '<span class="nav-item-type type-' + type + '">',
    type[0].toUpperCase(),
    '</span>',

    '<span class="nav-item-name">',
    typeLink,
    '</span>'
  ].join('')
}

/**
 @param {TAFFY} taffyData See <http://taffydb.com/>.
 @param {object} opts
 @param {Tutorial} tutorials
 */
exports.publish = function (taffyData, opts, tutorials) {
  data = taffyData

  var conf = env.conf.templates || {}
  conf.default = conf.default || {}

  var templatePath = path.normalize(opts.template)
  view = new template.Template(path.join(templatePath, "tmpl"))

  // claim some special filenames in advance, so the All-Powerful Overseer of Filename Uniqueness
  // doesn't try to hand them out later
  var indexUrl = helper.getUniqueFilename("index")

  // don't call registerLink() on this one! 'index' is also a valid longname
  var globalUrl = helper.getUniqueFilename("global")
  helper.registerLink("global", globalUrl)

  // set up templating
  view.layout = conf.default.layoutFile
    ? path.getResourcePath(
      path.dirname(conf.default.layoutFile),
      path.basename(conf.default.layoutFile)
    )
    : "layout.tmpl"

  // set up tutorials for helper
  helper.setTutorials(tutorials)
  data = helper.prune(data)
  data.sort("longname, version, since")
  helper.addEventListeners(data)

  var sourceFiles = {}
  var sourceFilePaths = []

  data().each(function (doclet) {
    doclet.attribs = ""

    if (doclet.examples) {
      doclet.examples = doclet.examples.map(function (example) {
        var caption, code

        if (
          example.match(
            /^\s*<caption>([\s\S]+?)<\/caption>(\s*[\n\r])([\s\S]+)$/i
          )
        ) {
          caption = RegExp.$1
          code = RegExp.$3
        }

        return {
          caption: caption || "",
          code: code || example
        }
      })
    }

    if (doclet.see) {
      doclet.see.forEach(function (seeItem, i) {
        doclet.see[i] = hashToLink(doclet, seeItem)
      })
    }

    // build a list of source files
    var sourcePath
    if (doclet.meta) {
      sourcePath = getPathFromDoclet(doclet)

      sourceFiles[sourcePath] = {
        resolved: sourcePath,
        shortened: null
      }

      if (sourceFilePaths.indexOf(sourcePath) === -1) {
        sourceFilePaths.push(sourcePath)
      }
    }
  })

  // update outdir if necessary, then create outdir
  var packageInfo = (find({kind: "package"}) || [])[0]
  if (packageInfo && packageInfo.name) {
    outdir = path.join(outdir, packageInfo.name, packageInfo.version || "")
  }
  fs.mkPath(outdir)

  // copy the template's static files to outdir
  var fromDir = path.join(templatePath, "static")
  var staticFiles = fs.ls(fromDir, 3)

  staticFiles.forEach(function (fileName) {
    var toDir = fs.toDir(fileName.replace(fromDir, outdir))
    fs.mkPath(toDir)
    fs.copyFileSync(fileName, toDir)
  })

  // copy user-specified static files to outdir
  var staticFilePaths
  var staticFileFilter
  var staticFileScanner
  if (conf.default.staticFiles) {
    // The canonical property name is `include`. We accept `paths` for backwards compatibility
    // with a bug in JSDoc 3.2.x.
    staticFilePaths = conf.default.staticFiles.include ||
      conf.default.staticFiles.paths || []
    staticFileFilter = new (require("jsdoc/src/filter").Filter)(
      conf.default.staticFiles
    )
    staticFileScanner = new (require("jsdoc/src/scanner").Scanner)()

    staticFilePaths.forEach(function (filePath) {
      var extraStaticFiles = staticFileScanner.scan(
        [filePath],
        10,
        staticFileFilter
      )

      extraStaticFiles.forEach(function (fileName) {
        var sourcePath = fs.toDir(filePath)
        var toDir = fs.toDir(fileName.replace(sourcePath, outdir))
        fs.mkPath(toDir)
        fs.copyFileSync(fileName, toDir)
      })
    })
  }

  if (sourceFilePaths.length) {
    sourceFiles = shortenPaths(sourceFiles, path.commonPrefix(sourceFilePaths))
  }

  data().each(function (doclet) {
    var url = helper.createLink(doclet)
    helper.registerLink(doclet.longname, url)

    // add a shortened version of the full path
    var docletPath
    if (doclet.meta) {
      docletPath = getPathFromDoclet(doclet)
      docletPath = sourceFiles[docletPath].shortened
      if (docletPath) {
        doclet.meta.shortpath = docletPath
      }
    }
  })

  data().each(function (doclet) {
    var url = helper.longnameToUrl[doclet.longname]

    if (url.indexOf("#") > -1) {
      doclet.id = helper.longnameToUrl[doclet.longname].split(/#/).pop()
    } else {
      doclet.id = doclet.name
    }

    if (needsSignature(doclet)) {
      addSignatureParams(doclet)
      addSignatureReturns(doclet)
      addAttribs(doclet)
    }
  })

  // do this after the urls have all been generated
  data().each(function (doclet) {
    doclet.ancestors = getAncestorLinks(doclet)

    if (doclet.kind === "member") {
      addSignatureTypes(doclet)
      addAttribs(doclet)
    }

    if (doclet.kind === "constant") {
      addSignatureTypes(doclet)
      addAttribs(doclet)
      doclet.kind = "member"
    }
  })

  var members = helper.getMembers(data)
  members.tutorials = tutorials.children

  // output pretty-printed source files by default
  var outputSourceFiles = conf.default &&
  conf.default.outputSourceFiles !== false
    ? true
    : false

  // add template helpers
  view.find = find
  view.linkto = linkto
  view.resolveAuthorLinks = resolveAuthorLinks
  view.tutoriallink = tutoriallink
  view.htmlsafe = htmlsafe
  view.outputSourceFiles = outputSourceFiles

  // once for all
  view.nav = buildNav(members)
  attachModuleSymbols(find({longname: {left: "module:"}}), members.modules)

  // generate the pretty-printed source files first so other pages can link to them
  if (outputSourceFiles) {
    generateSourceFiles(sourceFiles, opts.encoding)
  }

  if (members.globals.length) {
    generate("", "Global", [{kind: "globalobj"}], globalUrl)
  }

  // index page displays information from package.json and lists files
  var files = find({kind: "file"})
  var packages = find({kind: "package"})

  generate(
    "",
    "Home",
    packages
      .concat([
        {
          kind: "mainpage",
          readme: opts.readme,
          longname: opts.mainpagetitle ? opts.mainpagetitle : "Main Page"
        }
      ])
      .concat(files),
    indexUrl
  )

  // set up the lists that we'll use to generate pages
  var classes = taffy(members.classes)
  var modules = taffy(members.modules)
  var namespaces = taffy(members.namespaces)
  var mixins = taffy(members.mixins)
  var externals = taffy(members.externals)
  var interfaces = taffy(members.interfaces)

  Object.keys(helper.longnameToUrl).forEach(function (longname) {
    var myModules = helper.find(modules, {longname: longname})
    if (myModules.length) {
      generate(
        "Module",
        myModules[0].name,
        myModules,
        helper.longnameToUrl[longname]
      )
    }

    var myClasses = helper.find(classes, {longname: longname})
    if (myClasses.length) {
      generate(
        "Class",
        myClasses[0].name,
        myClasses,
        helper.longnameToUrl[longname]
      )
    }

    var myNamespaces = helper.find(namespaces, {longname: longname})
    if (myNamespaces.length) {
      generate(
        "Namespace",
        myNamespaces[0].name,
        myNamespaces,
        helper.longnameToUrl[longname]
      )
    }

    var myMixins = helper.find(mixins, {longname: longname})
    if (myMixins.length) {
      generate(
        "Mixin",
        myMixins[0].name,
        myMixins,
        helper.longnameToUrl[longname]
      )
    }

    var myExternals = helper.find(externals, {longname: longname})
    if (myExternals.length) {
      generate(
        "External",
        myExternals[0].name,
        myExternals,
        helper.longnameToUrl[longname]
      )
    }

    var myInterfaces = helper.find(interfaces, {longname: longname})
    if (myInterfaces.length) {
      generate(
        "Interface",
        myInterfaces[0].name,
        myInterfaces,
        helper.longnameToUrl[longname]
      )
    }
  })

  // TODO: move the tutorial functions to templateHelper.js
  function generateTutorial(title, tutorial, filename) {
    var tutorialData = {
      title: title,
      header: tutorial.title,
      content: tutorial.parse(),
      children: tutorial.children
    }

    var tutorialPath = path.join(outdir, filename)
    var html = view.render("tutorial.tmpl", tutorialData)

    // yes, you can use {@link} in tutorials too!
    html = helper.resolveLinks(html) // turn {@link foo} into <a href="foodoc.html">foo</a>
    fs.writeFileSync(tutorialPath, html, "utf8")
  }

  // tutorials can have only one parent so there is no risk for loops
  function saveChildren(node) {
    node.children.forEach(function (child) {
      generateTutorial(
        child.title,
        child,
        helper.tutorialToUrl(child.name)
      )
      saveChildren(child)
    })
  }

  saveChildren(tutorials)
};
