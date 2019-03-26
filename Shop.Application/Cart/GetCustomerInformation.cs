using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Shop.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Application.Cart
{
    public class GetCustomerInformation
    {
        private ISession _session;

        public GetCustomerInformation(ISession session)
        {
            _session = session;
        }

        public class Request
        {            
            public string FirstName { get; set; }            
            public string LastName { get; set; }            
            public string Email { get; set; }            
            public string PhoneNumber { get; set; }          
            
            public string Address1 { get; set; }            
            public string Address2 { get; set; }            
            public string City { get; set; }            
            public string PostCode { get; set; }
        }
        
        public Request Do()
        {
            var str = _session.GetString("customer-information");
            if (string.IsNullOrEmpty(str))
                return null;
            var response = JsonConvert.DeserializeObject<Request>(str);
            return response;
        }

    }
}
