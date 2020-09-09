using System;

namespace Stock.Services
{
    public class Response<TEntity>
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public DateTime Time { get; set; }
        public TEntity Data { get; set; }
    }
}