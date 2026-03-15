using keviofocus_backend.Data;
using keviofocus_backend.Dto.StudyMaterial;
using keviofocus_backend.Interfaces;
using keviofocus_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace keviofocus_backend.Service
{
    public class StudyMaterialService : IStudyMaterialService
    {

        private readonly AppDbContext _context;

        public StudyMaterialService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<StudyMaterialResponseDto>> GetAllBySession(Guid sessionId)
        {
            var materials = await _context.StudyMaterials
                .Where(m => m.SessionId == sessionId)
                .OrderByDescending(m => m.CreatedAt)
                .ToListAsync();

            return materials.Select(m => ToResponse(m)).ToList();
        }

        public async Task<StudyMaterialResponseDto?> GetById(Guid id)
        {
            var material = await _context.StudyMaterials.FindAsync(id);
            return material is null ? null : ToResponse(material);
        }

        public async Task<StudyMaterialResponseDto> Create(StudyMaterialCreateDto dto)
        {
            var material = new StudyMaterial
            {
                SessionId = dto.SessionId,
                Title = dto.Title,
                Type = dto.Type,
                Content = dto.Content,
                Note = dto.Note,
                CreatedAt = DateTime.UtcNow
            };

            _context.StudyMaterials.Add(material);
            await _context.SaveChangesAsync();
            return ToResponse(material);
        }

        public async Task<StudyMaterialResponseDto?> Update(Guid id, StudyMaterialUpdateDto dto)
        {
            var material = await _context.StudyMaterials.FindAsync(id);
            if (material is null) return null;

            if (dto.Title is not null) material.Title = dto.Title;
            if (dto.Content is not null) material.Content = dto.Content;
            if (dto.Note is not null) material.Note = dto.Note;

            await _context.SaveChangesAsync();
            return ToResponse(material);
        }

        public async Task<bool> Delete(Guid id)
        {
            var material = await _context.StudyMaterials.FindAsync(id);
            if (material is null) return false;

            _context.StudyMaterials.Remove(material);
            await _context.SaveChangesAsync();
            return true;
        }



        public static StudyMaterialResponseDto ToResponse(StudyMaterial m) => new()
        {
            Id = m.Id,
            SessionId = m.SessionId,
            Title = m.Title,
            Type = m.Type,
            Content = m.Content,
            Note = m.Note,
            CreatedAt = m.CreatedAt
        };
    }
}
