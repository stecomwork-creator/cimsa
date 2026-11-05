using System;
using System.Collections.Generic;
using System.Text;

namespace Placeholder.SupplierApplications
{
    // SCRUM-182 taslak kod: Gerçek DTO/entity/servis adları projeye göre uyarlanmalıdır.
    public static class SupplierApplicationTextNormalizer
    {
        private static readonly Dictionary<char, char> TurkishToEnglish = new()
        {
            ['ç'] = 'C', ['Ç'] = 'C',
            ['ğ'] = 'G', ['Ğ'] = 'G',
            ['ı'] = 'I', ['İ'] = 'I',
            ['ö'] = 'O', ['Ö'] = 'O',
            ['ş'] = 'S', ['Ş'] = 'S',
            ['ü'] = 'U', ['Ü'] = 'U'
        };

        public static string? NormalizeCompanyTitleOrTaxOffice(string? value)
        {
            if (value is null)
                return null;

            var trimmed = value.Trim();
            if (trimmed.Length == 0)
                return string.Empty;

            var builder = new StringBuilder(trimmed.Length);

            foreach (var character in trimmed)
            {
                if (TurkishToEnglish.TryGetValue(character, out var englishCharacter))
                {
                    builder.Append(englishCharacter);
                    continue;
                }

                builder.Append(char.ToUpperInvariant(character));
            }

            return builder.ToString();
        }
    }

    // Placeholder: Tedarikçi başvuru kayıt/güncelleme akışında çağrılacak örnek kullanım.
    public sealed class SupplierApplicationCommand
    {
        public string? FirmaAcikUnvani { get; set; }
        public string? VergiDairesi { get; set; }
        public string? Email { get; set; }
        public string? WebAdresi { get; set; }
        public string? SirketYetkilisi { get; set; }
    }

    public sealed class SupplierApplicationService
    {
        public void CreateOrUpdate(SupplierApplicationCommand command)
        {
            // Yalnızca yeni kayıt ve güncelleme sırasında çalışır; mevcut kayıtlar için toplu dönüşüm yapmaz.
            command.FirmaAcikUnvani = SupplierApplicationTextNormalizer.NormalizeCompanyTitleOrTaxOffice(command.FirmaAcikUnvani);
            command.VergiDairesi = SupplierApplicationTextNormalizer.NormalizeCompanyTitleOrTaxOffice(command.VergiDairesi);

            // E-posta, web adresi ve diğer metin alanları bu talep kapsamında değiştirilmez.
            // TODO: Mevcut proje repository/save metodu burada çağrılmalıdır.
            // _supplierApplicationRepository.Save(command);
        }
    }
}
