using LMS.DAL.Models.Dto.Common;
using LMS.DAL.Models.Dto.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.BAL.Interfaces
{
    public interface ILibrary
    {
        Task<CreateOrEditLibrary> CreateOrEdit(CreateOrEditLibrary library);
        Task<List<GetAllBooksDto>> GetAllBooks(LibraryListDto common);
        Task<CreateOrEditLibrary> GetSingleBookRecord(Guid id);
    }
}
