using AddressBook.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace AddressBook.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AddressesController : ControllerBase
    {
        /// Bellek içi veritabanı.
        private static List<Adress> _addresses = new List<Adress>
        {
            new Adress { Id = 1, FirstName = "Irmak", LastName = "Arabacı", PhoneNumber = "123456789", City = "İzmir", District = "Bornova" },
            new Adress { Id = 2, FirstName = "Ülkü Bartu", LastName = "Serbest", PhoneNumber = "987654321", City = "Ankara", District = "Çankaya" }
        };

        /// Tüm adresleri listeler.
        [HttpGet]
        public IActionResult GetAllAddresses()
        {
            return Ok(_addresses);
        }

        /// Belirtilen ID'ye sahip adresi getirir.
        [HttpGet("{id}")]
        public IActionResult GetAddressById(int id)
        {
            var address = _addresses.FirstOrDefault(a => a.Id == id);
            if (address == null)
            {
                return NotFound($"ID'si {id} olan adres bulunamadı.");
            }
            return Ok(address);
        }

        /// Ad, soyad, şehir veya ilçeye göre arama yapar.
        [HttpGet("search")]
        public IActionResult SearchAddresses([FromQuery] string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return Ok(new List<Adress>());
            }
            var lowerCaseQuery = query.Trim().ToLowerInvariant();
            var results = _addresses.Where(a =>
                a.FirstName.ToLowerInvariant().Contains(lowerCaseQuery) ||
                a.LastName.ToLowerInvariant().Contains(lowerCaseQuery) ||
                a.City.ToLowerInvariant().Contains(lowerCaseQuery) ||
                a.District.ToLowerInvariant().Contains(lowerCaseQuery)
            ).ToList();
            return Ok(results);
        }

        /// Yeni bir adres oluşturur.
        [HttpPost]
        public IActionResult CreateAddress([FromBody] Adress newAddress)
        {
            if (newAddress == null)
            {
                return BadRequest("Adres bilgileri boş olamaz.");
            }
            var newId = _addresses.Any() ? _addresses.Max(a => a.Id) + 1 : 1;
            newAddress.Id = newId;
            _addresses.Add(newAddress);
            return CreatedAtAction(nameof(GetAddressById), new { id = newId }, newAddress);
        }

        /// Bir adresi bütünüyle günceller.
        [HttpPut("{id}")]
        public IActionResult UpdateAddress(int id, [FromBody] Adress updatedAddress)
        {
            var address = _addresses.FirstOrDefault(a => a.Id == id);
            if (address == null)
            {
                return NotFound($"ID'si {id} olan adres bulunamadı.");
            }
            address.FirstName = updatedAddress.FirstName;
            address.LastName = updatedAddress.LastName;
            address.PhoneNumber = updatedAddress.PhoneNumber;
            address.City = updatedAddress.City;
            address.District = updatedAddress.District;
            return NoContent();
        }

        /// Bir adresin belirli alanlarını kısmi olarak günceller (Edit).
        [HttpPatch("{id}")]
        public IActionResult EditAddress(int id, [FromBody] JsonPatchDocument<Adress> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest("Değişiklik bilgileri boş olamaz.");
            }
            var address = _addresses.FirstOrDefault(a => a.Id == id);
            if (address == null)
            {
                return NotFound($"ID'si {id} olan adres bulunamadı.");
            }

            patchDoc.ApplyTo(address, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return NoContent();
        }

        /// Belirtilen ID'ye sahip adresi siler.
        [HttpDelete("{id}")]
        public IActionResult DeleteAddress(int id)
        {
            var address = _addresses.FirstOrDefault(a => a.Id == id);
            if (address == null)
            {
                return NotFound($"ID'si {id} olan adres bulunamadı.");
            }
            _addresses.Remove(address);
            return NoContent();
        }
    }
}