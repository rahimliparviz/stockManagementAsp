using System;
using System.Linq;
using System.Net;
using Stock.Data;
using Stock.Domain;
using Stock.Services.Errors;
using Stock.Services.Repositories.Abstract;

namespace Stock.Services.Repositories.Concrete
{
    public class RegulationRepository:IRegulationRepository
    {
        private readonly DataContext _context;

        public RegulationRepository(
            DataContext context
           )
        {
            _context = context;
        }

        public Regulation Regulations()
        {
            var regulations = _context.Regulations.FirstOrDefault();
            
            _= regulations ?? throw new RestException(HttpStatusCode.NotFound, new { Regulations
                = "Not found" });
            
            return _context.Regulations.First();
        }

        public Response<Regulation> Create(Regulation reg)
        {
            var regulation = _context.Regulations.FirstOrDefault();

            if (regulation == null)
            {
                regulation = new Regulation()
                {
                    Address = reg.Address,
                    Email = reg.Email,
                    Vat = reg.Vat,
                    Phone = reg.Phone,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };

                _context.Regulations.Add(regulation);
            }
            else
            {
                regulation.Address = reg.Address;
                regulation.Email = reg.Email;
                regulation.Vat = reg.Vat;
                regulation.Phone = reg.Phone;
                regulation.UpdatedAt = DateTime.Now;
            }

            _context.SaveChanges();
            
            
            
            return new Response<Regulation>
            {
                Data = regulation,
                Message = "Regulations set",
                Time = DateTime.Now,
                IsSuccess = true
            };
        }
    }
}