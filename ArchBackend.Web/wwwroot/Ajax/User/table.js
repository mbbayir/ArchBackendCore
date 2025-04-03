﻿function format(d) {
    console.log(d);

    var cou = d.Countries,
        Countries = "";
    
        
    cou.forEach(element => {
        Countries += `<span class="badge bg-primary">${element.Country.name}</span><br>`;
    }
    );

    var TableTr = "";
    if (Countries != "") {
        TableTr = `<tr>
        <td> Countries</td>
        <td>${Countries}</td>
    </tr>`;
    }


    var table = `<table class="table table-bordered text-start">
        <tbody>
            <tr>
                 <td> Editing</td>
                 <td>${d.editingDate ? moment(d.editingDate).format('DD/MM/YYYY') : null}</td>
            </tr>
            ${TableTr}
            </tbody>
        </table>`;
    return table;
}






$(document).ready(function () {
    // setting();

    $('#example').on('click', 'tbody td.dt-control', function () {
        var tr = $(this).closest('tr');
        var row = table.row(tr);

        if (row.child.isShown()) {
            // This row is already open - close it
            row.child.hide();
        }
        else {
            // Open this row
            row.child(format(row.data())).show();
        }
    });
    $('#example').on('requestChild.dt', function (e, row) {
        row.child(format(row.data())).show();
    });


    var userHasSorted = false;
    var table = $('#example').DataTable({
        "preDrawCallback": function (settings) {
            var api = new $.fn.dataTable.Api(settings);
            if (!userHasSorted) {
                api.order([]); // Başlangıçta sıralama yapılmamasını sağlar
            }
        },
        "ajax": {
            "url": "/User/TableData",
            "type": "POST",
            "datatype": "json",
            "data": function (d) {
                if (userHasSorted) {
                    d.orderColumnIndex = d.order[0].column;
                    d.orderColumnName = d.columns[d.orderColumnIndex].data;
                    d.orderDir = d.order[0].dir;
                }
                d.eventId = $("#EventId").val();
            },
        },
        "rowId": 'id',
        "columns": [
            {
                "className": 'dt-control',
                "orderable": false,
                "searchable": false,
                "data": null,
                "defaultContent": ''
            },
            {
                "data": null,  render: function (row) {
                    return row.name + " " + row.surname;
                }
            },
            { "data": "userName" },
            {
                "orderable": false,
                "searchable": false,
                "data": null, "render": function (row) {

                    var id = row.id,
                    buttons = `
                        <a class='btn btn-warning me-1' href="/UserUpdate/${id}" data-bs-toggle="tooltip" title="Update User"><i class="fa-solid fa-pen"></i></a>
                        <button type="button" class="btn btn-danger deleteButton me-1" data-id="${id}" data-bs-toggle="tooltip" title="Delete User">
                            <i class="fa-solid fa-trash"></i>
                        </button>
                    `;
                return buttons;
                

                }
            }
        ],
        "language": {
            "url": "/Ajax/_dataTable/language.json"
        },
        "draw": 1,
        "processing": true,
        "serverSide": true,
        "searchable": true,
        "search": {
            "value": "my search value",
            "regex": false
        },
        "stateSave": true,
        "pageLength": 25,

    });


    // start();
    table.on('stateLoaded', (e, settings, data) => {
        for (var i = 0; i < data.childRows.length; i++) {
            var row = table.row(data.childRows[i]);
            row.child(format(row.data())).show();
        }
    });
    table.on('click', 'th', function () {
        userHasSorted = true;
    });

    $('#example tbody').on('click', '.deleteButton', function (e) {
        e.preventDefault();
        var id = $(this).data('id');


        iziToast.question({
            timeout: 20000,
            close: false,
            overlay: true,
            displayMode: 'once',
            id: 'question',
            zindex: 999,
            title: 'Hey',
            message: 'Are you sure about that?',
            position: 'center',
            buttons: [
                ['<button><b>YES</b></button>', function (instance, toast) {

                    instance.hide({ transitionOut: 'fadeOut' }, toast, 'button');
                    $.ajax({
                        type: "POST",
                        url: '/User/RemoveJson',
                        data: {
                            "id": id
                        },
                        success: function (response) {
                            if (response.success) {
                                table.ajax.reload();
                                iziToast.success({ timeout: 4000, icon: 'fas fa-check', title: 'Success!', message: response.message });
                            }
                            else {
                                iziToast.warning({ timeout: 4000, icon: 'fas fa-times', title: 'Warning!', message: response.message });
                            }
                        }
                    });

                }, true],
                ['<button>NO</button>', function (instance, toast) {

                    instance.hide({ transitionOut: 'fadeOut' }, toast, 'button');

                }],
            ],
            onClosing: function (instance, toast, closedBy) {
                console.info('Closing | closedBy: ' + closedBy);
            },
            onClosed: function (instance, toast, closedBy) {
                console.info('Closed | closedBy: ' + closedBy);
            }
        });


    });


});



