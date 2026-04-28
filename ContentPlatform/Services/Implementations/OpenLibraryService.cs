using ContentPlatform.Dto_s;
using ContentPlatform.Enums;
using ContentPlatform.ExternalApi.OpenLibrary;
using ContentPlatform.Helpers;
using ContentPlatform.Services.Interfaces;
using System.Reflection.Metadata.Ecma335;

namespace ContentPlatform.Services.Implementations
{
    public class OpenLibraryService(HttpClient httpClient) : IOpenLibraryService
    {
        public async Task<List<BookCreateDto>> GetPopularBooks(int count)
        {
            var url = $"https://openlibrary.org/search.json?q=classic+fiction&limit={count}&sort=rating" +
              "&fields=key,title,author_name,publisher,first_publish_year,number_of_pages_median,language,cover_i,subject"; ;
            var response = await httpClient.GetFromJsonAsync<OpenLibraryResponse>(url);
            return response?.Docs?.Select(MapToDto).ToList() ?? new List<BookCreateDto>();

        }
        private BookCreateDto MapToDto(OpenLibraryDoc doc)
        {
            var publisher = doc.Publishers?.FirstOrDefault(p => p.Length > 3) ?? BookGenre.GloBalClassic.ToString();
            return new BookCreateDto
            {
                Title = doc.Title ?? "Unknown",
                Description = $"Твір '{doc.Title}' вперше опублікований у {doc.FirstPublishYear}",
                BookAuthor = string.Join(", ", doc.AuthorNames ?? ["Unknown"]),
                BookPublisher = publisher,
                BookOriginalLanguage = doc.Languages?.FirstOrDefault() ?? "Unknown",
                BookPages = doc.Pages ?? 250,
                ReleaseYear = doc.FirstPublishYear ?? DateTime.Now.Year,
                ReleaseDate = doc.FirstPublishYear.HasValue ?
                    DateOnly.FromDateTime(new DateTime(doc.FirstPublishYear.Value, 1, 1)) :
                    DateOnly.FromDateTime(DateTime.Now),
                ImageUrl = doc.CoverId.HasValue ? $"https://covers.openlibrary.org/b/id/{doc.CoverId}-L.jpg" : null,
                ExternalId = $"ol-{doc.Key.Split('/').Last()}",
                BookGenres = MapGenres(doc.Subject),
            };
        }
        private List<BookGenre> MapGenres(List<string>? subjects)
        {
            if (subjects == null)
            {
                Console.WriteLine("Subjects list is null or empty");
                return new List<BookGenre>();
            }
                
            var result = new List<BookGenre>();
            var allSubjects = string.Join(" ", subjects).ToLower();
            Console.WriteLine($"Raw subjcts:  { allSubjects}");
            foreach (var entry in BookGenreMap.Keywords)
            {
                if(entry.Value.Any(word => allSubjects.Contains(word)))
                {
                    result.Add(entry.Key);
                }
              
            }
            if (!result.Any())
            {
                result.Add(BookGenre.Other);
            }
            return result.Distinct().ToList();
        }
    }
}
