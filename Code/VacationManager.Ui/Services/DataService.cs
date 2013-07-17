using System;
using Csla;
using VacationManager.Ui.Results;

namespace VacationManager.Ui.Services
{
    public class DataService : IDataService
    {
        public FetchListResult<TList, TObject> FetchList<TList, TObject>(object criteria)
            where TList : ReadOnlyListBase<TList, TObject>
            where TObject : BusinessBase<TObject>
        {
            if (criteria == null)
                return new FetchListResult<TList, TObject>();

            return new FetchListResult<TList, TObject>(criteria);
        }
        
        public ExecuteResult<TObject> Execute<TObject>(TObject obj)
            where TObject : CommandBase<TObject>
        {
            return new ExecuteResult<TObject>(obj);
        }
        
        public LoginResult Login(string user, string password)
        {
            return new LoginResult(user, password);
        }

        #region CRUD helpers for business objects

        public CreateResult<TObject> Create<TObject>() 
            where TObject : BusinessBase<TObject>
        {
            return new CreateResult<TObject>();
        }

        public FetchResult<TObject> Fetch<TObject>(long id)
            where TObject : BusinessBase<TObject>
        {
            return new FetchResult<TObject>(id);
        }

        public UpdateResult<TObject> Update<TObject>(TObject obj) 
            where TObject : BusinessBase<TObject>
        {
            return new UpdateResult<TObject>(obj);
        }

        public DeleteResult<TObject, TCriteria> Delete<TObject, TCriteria>(TObject obj, Func<TObject, TCriteria> selector) 
            where TObject : BusinessBase<TObject>
        {
            return new DeleteResult<TObject, TCriteria>(obj, selector);
        }

        #endregion
    }
}