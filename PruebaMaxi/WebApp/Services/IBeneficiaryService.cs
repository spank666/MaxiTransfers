using WebApp.Dtos;

namespace WebApp.Services
{
    public interface IBeneficiaryService
    {
        Task<List<BeneficiaryDto>> AllBeneficiariesOfEmployee(int id);
        Task<bool> Add(BeneficiaryDto beneficiary);
        Task<bool> Delete(int Id);
        Task<bool> Update(BeneficiaryDto beneficiary);
        Task<bool> UpdatePercentage(List<BeneficiaryDto> items);
    }
}
