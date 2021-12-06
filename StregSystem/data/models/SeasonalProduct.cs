using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StregSystem.data.models
{
    class SeasonalProduct : Product
    {
        public SeasonalProduct(Product product, string seasonStartDate, string seasonEndDate)
            : base(product.ID, product.Name, product.Price, product.CanBeBoughtOnCredit, product.Active)
        {
            SetDates(seasonStartDate, seasonEndDate);
            ShouldBeActive();
        }
        public DateTime SeasonStarttDate { get; private set; }
        public DateTime SeasonEndDate { get; private set; }
        private void ShouldBeActive()
        {
            if(DateTime.Now >= SeasonStarttDate && DateTime.Now <= SeasonEndDate)
            {
                this.Active = true;
            }
            else
            {
                this.Active = false;
            }
        }
        private void SetDates(string startDate, string EndDate)
        {
            SeasonStarttDate = DateConverter(startDate);
            SeasonEndDate = DateConverter(EndDate) > SeasonStarttDate? 
                DateConverter(EndDate) : throw new ArgumentException("End date needs to be after StartDate");
        }
        private DateTime DateConverter(string dates)
        {
            DateTime dt;            
            if(!DateTime.TryParse(dates, out dt))
            {
                throw new ArgumentException("Datetime is not in the correct format" + dates);
            }
            return dt;            
        }
    }
    
}
