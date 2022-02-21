
using System.Collections.Generic;

namespace CAR_RENTAL_SERVICE.Infrastructure
{
    // public interface ICRUDRepository_Billing<BillingDetail, String>
    // {
    //     IEnumerable<BillingDetail> GetAll();
    //     BillingDetail GetDetails(string id);
    //     void Create(BillingDetail item);
    //     void Update(BillingDetail item);
    //     void Delete(string id);
    // }
    // public interface ICRUDRepository_Booking<BookingDetail, String>
    // {
    //     IEnumerable<BookingDetail> GetAll();
    //     BookingDetail GetDetail(string id);
    //     void Create(BookingDetail item);
    //     void Update(BookingDetail item);
    //     void Delete(string id);
    // }
    public interface ICRUDRepository<TEntity, TIdentity>

    {

        IEnumerable<TEntity> GetAll();

        TEntity GetDetails(TIdentity id);

        void Create(TEntity item);

        void Update(TEntity item);

        void Delete(TIdentity id);

    }
}