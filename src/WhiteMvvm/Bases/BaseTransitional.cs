using System;
using System.Collections.Generic;
using System.Text;

namespace WhiteMvvm.Bases
{
    public class BaseTransitional
    {
        /// <summary>
        /// convert transitional object to model object
        /// </summary>
        /// <typeparam name="TBaseModel"></typeparam>
        /// <returns></returns>
        public virtual TBaseModel ToModel<TBaseModel>() where TBaseModel : BaseModel, new()
        {
            return new TBaseModel();
        }
    }
}
