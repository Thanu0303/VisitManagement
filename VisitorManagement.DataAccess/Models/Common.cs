using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace VisitorManagement.DataAccess.Models
{
    public class Common<T, U>
    {
        //Maps the properties having same name and data type From Source to destination
        public U Source_Target(T Source)
        {
            try
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<T, U>();
                });
                IMapper iMapper = config.CreateMapper();
                //var source = new Models.Admin_View();
                var destination = iMapper.Map<T, U>(Source);
                return destination;
            }
            catch (Exception exception)
            {

                return default;
            }
        }
        //Mapping For List of Models from Source to destination
        public List<U> List_Source_Target(List<T> Source)
        {
            try
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<T, U>();
                });
                IMapper iMapper = config.CreateMapper();
                //var source = new Models.Admin_View();
                var destination = iMapper.Map<List<T>, List<U>>(Source);
                return destination;
            }
            catch (Exception exception)
            {
                return default;
            }
        }
    }
}
