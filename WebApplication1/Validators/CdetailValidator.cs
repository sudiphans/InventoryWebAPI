using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using EmployeeService.Models;
using EmployeeService.infrastructure;
using EmployeeService.Controllers;
using System.Text;

namespace EmployeeService.Validators
{
    public class CdetailValidator : AbstractValidator<CDetail>
    {
        private readonly InventoryContext _db;


        public CdetailValidator(InventoryContext db)
        {
            //implementing db context for database operation
            _db = db;


            RuleFor(x => x.Lb_no)
                .NotEmpty().WithMessage("Cant be emplty");
            // .Must(UniqueLbNo).WithMessage("Lb No must be unique.");



            RuleFor(y => y.sl_no)
               // .NotEmpty().WithMessage("Cant be empty");
                .Must(UniqueSlNo)
               .WithMessage("CPU serial number must be unique.");

           

        }


        private bool UniqueSlNo(string sl_no)
        {
            Console.WriteLine("sudip msg"+sl_no);
            var result = (from d in _db.CDetails where d.sl_no == sl_no select d);

            if (!result.Any())
            {
                return true;
            }
            else
            {
                return false;
            }
           

         }

        private bool UniqueLbNo(int Lb_no)
        {

            var result = (from d in _db.CDetails where d.Lb_no == Lb_no select d);

            if (!result.Any())
            {
                return true;
            }
            else
            {
                return false;
            }

        }






    }
}
