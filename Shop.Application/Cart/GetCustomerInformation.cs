using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Shop.Database;
using Shop.Domain.Models;
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

        public class Response
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
        
        
        public Response Do()
        {
            var str = _session.GetString("customer-information");
            if (string.IsNullOrEmpty(str))
                return null;
            var customerInfo = JsonConvert.DeserializeObject<CustomerInformation>(str);
            return new Response
            {
                FirstName = customerInfo.FirstName,
                LastName = customerInfo.LastName,
                Email = customerInfo.Email,
                PhoneNumber = customerInfo.PhoneNumber,
                Address1 = customerInfo.Address1,
                Address2 = customerInfo.Address2,
                City = customerInfo.City,
                PostCode = customerInfo.PostCode
            };
        }

    }
} 
