using System;
using System.Net;

namespace Stock.Services.Errors
{
    //yalniz obyekt constructur vasitesile yaradila blsin deye readonly fieldlerden istifade olunur
    //daha sora public oxunmasi seraiti yaradilir
    public class RestException :  Exception
    {
        private readonly HttpStatusCode _code;
        private readonly object _errors;


        public RestException(HttpStatusCode code,object errors = null)
        {
            _code = code;
            _errors = errors;
        }

        public HttpStatusCode Code => _code;

        public object Errors => _errors;
    }
}