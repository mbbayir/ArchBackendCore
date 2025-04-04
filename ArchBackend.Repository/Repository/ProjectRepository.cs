using ArchBackend.Core.Models;
using ArchBackend.Core.Repositories;
using ArchBackend.Repository.Repository.Generic;
using Microsoft.EntityFrameworkCore;

public class ProjectRepository : GenericRepository<Project>, IProjectRepository
{
    private readonly AppDbContext _context;

    public ProjectRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<Project?> GetProjectWithPicturesByIdAsync(int id)
    {
        return await _context.Projects
            .Include(p => p.Picture)
            .FirstOrDefaultAsync(p => p.Id == id);
    }
    public async Task<Project> AddAsync(Project project)
    {
        await _context.Projects.AddAsync(project);
        await _context.SaveChangesAsync();
        return project;
    }


    public async Task<IEnumerable<Project>> GetAllAsync()
    {
        return await _context.Projects.ToListAsync();
    }

    public async Task<bool> UpdateAsync(Project project)
    {
        _context.Projects.Update(project);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task DeleteAsync(Project project)
    {
        _context.Projects.Remove(project);
        await _context.SaveChangesAsync();
    }
}
