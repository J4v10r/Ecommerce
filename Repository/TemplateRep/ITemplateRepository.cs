using Saas_Sexshop.Models;

namespace Saas_Sexshop.Repository.TemplateRep
{
    public interface ITemplateRepository
    {
        Task AddTemplateAsync(Template template);
        Task<bool>DeleteTemplateByIdAsync(int templateId);
        Task UpdateTemplateAsync(Template template);
        Task<Template?> GetTemplateByIdAsync(int templateId);
        Task<IEnumerable<Template>> GetAllTemplatesAsync();
        Task<IEnumerable<Template>> GetActiveTemplatesAsync();
        Task<Template?> GetTemplateByNameAsync(string templateName);
    }
}
