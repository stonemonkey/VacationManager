using System;
using Csla;
using VacationManager.Ui.Results;

namespace VacationManager.Ui.Services
{
    public interface IDataService
    {
        FetchListResult<TList, TObject> FetchList<TList, TObject>(object criteria)
            where TList : ReadOnlyListBase<TList, TObject>
            where TObject : BusinessBase<TObject>;
        
        ExecuteResult<TObject> Execute<TObject>(TObject obj)
            where TObject : CommandBase<TObject>;

        LoginResult Login(string user, string password);

        #region CRUD helpers for business objects

        CreateResult<TObject> Create<TObject>()
            where TObject : BusinessBase<TObject>;

        FetchResult<TObject> Fetch<TObject>(long id)
            where TObject : BusinessBase<TObject>;

        UpdateResult<TObject> Update<TObject>(TObject obj)
            where TObject : BusinessBase<TObject>;

        DeleteResult<TObject, TCriteria> Delete<TObject, TCriteria>(TObject obj, Func<TObject, TCriteria> selector)
            where TObject : BusinessBase<TObject>;

        #endregion
    }
}