using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Builder;
using Microsoft.OData.Edm;

namespace EmployeeService.Models
{
    public class CdetailModelBuilder
    {
        public IEdmModel GetEdmModel(IServiceProvider serviceProvider)
        {
            var builder = new ODataConventionModelBuilder(serviceProvider);
            builder.EntitySet<CDetail>("CDetails");



            builder.EntitySet<CLoan>("CLoans");
               

            return builder.GetEdmModel();
        }


    }
}
