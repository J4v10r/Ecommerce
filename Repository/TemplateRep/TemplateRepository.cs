using Microsoft.EntityFrameworkCore;
using Saas_Sexshop.Infrastructure.Context;
using Saas_Sexshop.Models;

namespace Saas_Sexshop.Repository.TemplateRep
{
    public class TemplateRepository : ITemplateRepository
    {

        private readonly AppDbContext _context;
        private readonly ILogger<TemplateRepository> _logger;


        public TemplateRepository(AppDbContext context, ILogger<TemplateRepository> logger){
            _context = context;
            _logger = logger;
        }


        public async Task AddTemplateAsync(Template template)
        {
            try
            {
                await _context.AddAsync(template);
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Template com {template.TemplateId} adicionado com sucesso");
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, $"Erro ao adicionar template de id {template.TemplateId} no banco de dados.");
                throw new DbUpdateException("Ocorreu um erro ao adicionar o template. Tente novamente mais tarde.", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro inesperado ao adicionar Template de ID {template.TemplateId}",template.TemplateId);
                throw new ApplicationException("Ocorreu um erro inesperado. Tente novamente mais tarde.", ex);
            }
        }

        public async Task<bool> DeleteTemplateByIdAsync(int templateId)
        {
            try
            {
                var template = await _context.Templates.FindAsync(templateId);
                if (template == null)
                {
                    _logger.LogWarning($"template com ID {templateId} não encontrado.");
                    return false;
                }
                _context.Remove(template);
                await _context.SaveChangesAsync();
                _logger.LogInformation($"template com ID  {templateId} foi removido com sucesso");
                return true;

            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, $" Erro ao remorcer template com ID  {templateId} do banco de dados.");
                throw new Exception($"Erro ao remover template com ID  {templateId} do banco de dados.", ex);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro inesperado ao remover template com ID  {templateId} do banco de dados;");
                throw new ApplicationException($"Erro inesperado ao remover template com ID {templateId} do banco de dados;", ex);
            }
        }

        public async Task<IEnumerable<Template>> GetActiveTemplatesAsync()
        {
            try
            {
                var activeTemplates = await _context.Templates.AsNoTracking().Where(t => t.IsActive).ToListAsync();

                if (!activeTemplates.Any())
                {
                    _logger.LogWarning("Nenhum template ativo encontrado.");
                }

                return activeTemplates;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar templates ativos.");
                throw new ApplicationException("Erro ao buscar templates ativos. Tente novamente mais tarde.", ex);
            }
        }

        public async Task<IEnumerable<Template>> GetAllTemplatesAsync()
        {
            try
            {
                var templates = await _context.Templates.AsNoTracking().ToListAsync();

                if (!templates.Any())
                {
                    _logger.LogWarning("Nenhum template encontrado no banco de dados.");
                }

                return templates;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar todos os templates.");
                throw new ApplicationException("Erro ao buscar templates. Tente novamente mais tarde.", ex);
            }
        }

        public async Task<Template?> GetTemplateByIdAsync(int templateId){
            try
            {
                var template = await _context.Templates.AsNoTracking().FirstOrDefaultAsync(t => t.TemplateId == templateId);

                if (template == null)
                {
                    _logger.LogWarning("Template com ID {TemplateId} não encontrado.", templateId);
                }

                return template;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar o template com ID {TemplateId}.", templateId);
                throw new ApplicationException("Erro ao buscar o template. Tente novamente mais tarde.", ex);
            }
        }

        public async Task<Template?> GetTemplateByNameAsync(string templateName)
        {
            try
            {
                var template = await _context.Templates.AsNoTracking().FirstOrDefaultAsync(t => t.Name == templateName);

                if (template == null)
                {
                    _logger.LogWarning($"Template com nome {templateName} não encontrado.");
                }

                return template;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao buscar o template com nome {templateName}");
                throw new ApplicationException("Erro ao buscar o template. Tente novamente mais tarde.", ex);
            }
        }

        public async Task UpdateTemplateAsync(Template template)
        {
            try
            {
                var existingTemplate = await _context.Templates.FindAsync(template.TemplateId);
                if (existingTemplate == null)
                {
                    _logger.LogWarning("Tentativa de atualizar um template inexistente. ID: {TemplateId}", template.TemplateId);
                    throw new KeyNotFoundException($"Template com ID {template.TemplateId} não encontrado.");
                }

                _context.Entry(existingTemplate).CurrentValues.SetValues(template);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Template com ID {TemplateId} atualizado com sucesso.", template.TemplateId);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Erro ao atualizar template ID {TemplateId}.", template.TemplateId);
                throw new ApplicationException("Erro ao atualizar template. Tente novamente mais tarde.", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro inesperado ao atualizar template ID {TemplateId}.", template.TemplateId);
                throw new ApplicationException("Erro inesperado ao atualizar template.", ex);
            }
        }
    }
}
