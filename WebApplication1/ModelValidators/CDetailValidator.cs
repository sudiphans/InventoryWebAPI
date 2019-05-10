using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeService.Models;
using EmployeeService.infrastructure;
using FluentValidation;

namespace EmployeeService.ModelValidators
{
    public class CDetailValidator : AbstractValidator<CDetail>
    {

        private readonly InventoryContext _db;


        public CDetailValidator( InventoryContext db)
        {
            _db = db;
            
            RuleFor(m => m.Lb_no)
                .NotEmpty().WithMessage("Cant be empty")
                .Must(UniqueLbNo).WithMessage("Must be unique")
             ;
        }


        private bool UniqueLbNo(int Lb_no)
        {
            var result = (from d in _db.CDetails where d.Lb_no == Lb_no select d);

            if(!result.Any())
            {
                return true;
            }else
            {

                return false;
            }


        }


    }
}
