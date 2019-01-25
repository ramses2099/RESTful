using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.GenericRepository
{
    public class GenericRepository<TEntity> where TEntity : class
    {

        #region Private member variables

        internal OnLineStoreEntities Context;
        internal DbSet<TEntity> DbSet;

        #endregion

        #region Public Constructor

        public GenericRepository(OnLineStoreEntities context)
        {
            this.Context = context;
            this.DbSet = context.Set<TEntity>();
        }

        #endregion

        #region Public member methods

        /// <summary>
        /// Generic Get method for entities
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<TEntity> Get()
        {
            IQueryable<TEntity> query = DbSet;
            return query.ToList();
        }

        /// <summary>
        /// Generic get method on the basic of id for entities
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual TEntity GetByID(object id)
        {
            return DbSet.Find(id);
        }

        /// <summary>
        /// Generic Insert method for the entities
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Insert(TEntity entity)
        {
            DbSet.Add(entity);
        }

        /// <summary>
        /// Generic Delete method for the entities
        /// </summary>
        /// <param name="id"></param>
        public virtual void Delete(object id)
        {
            TEntity entityDelete = DbSet.Find(id);
            Delete(entityDelete);

        }

        /// <summary>
        /// Generic Delete method for the entities
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Delete(TEntity entity)
        {
            if (Context.Entry(entity).State == EntityState.Detached)
            {
                DbSet.Attach(entity);
            }
            DbSet.Remove(entity);
        }

        /// <summary>
        /// Generic update method for the entities
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Update(TEntity entity)
        {

            DbSet.Attach(entity);
            Context.Entry(entity).State = EntityState.Modified;

        }

        /// <summary>
        /// Generic method to get many record on the basis of a condition
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public virtual IEnumerable<TEntity> GetMany(Func<TEntity, bool> where)
        {

            return DbSet.Where(where).ToList();
        }

        /// <summary>
        /// Generic method to get many record on the basis of a condition but query able
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public virtual IQueryable<TEntity> GetManyQueryable(Func<TEntity, bool> where)
        {
            return DbSet.Where(where).AsQueryable();

        }

        /// <summary>
        /// Generic get method, fetches data for the entities on the basis of condition
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public TEntity Get(Func<TEntity, Boolean> where) {

            return DbSet.Where(where).FirstOrDefault<TEntity>();
        }

        /// <summary>
        /// Generic delte method, deletes data for the entities on the basis of condition
        /// </summary>
        /// <param name="where"></param>
        public void Delete(Func<TEntity, Boolean> where) {

            IQueryable<TEntity> objects = DbSet.Where<TEntity>(where).AsQueryable();
            foreach (TEntity obj in objects)
                DbSet.Remove(obj);
        }

        /// <summary>
        /// Generic method to fetch all the records from db
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<TEntity> GetAll() {

            return DbSet.ToList();
        }

        /// <summary>
        /// Include multiple
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="include"></param>
        /// <returns></returns>
        public IQueryable<TEntity> GetWithInclude(
            System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate, params string[] include)
        {
            IQueryable<TEntity> query = this.DbSet;
            query = include.Aggregate(query, (current, inc) => current.Include(inc));
            return query.Where(predicate);
        }

        /// <summary>
        ///  Generic mEthod to check if entity exists
        /// </summary>
        /// <param name="primaryKey"></param>
        /// <returns></returns>
        public bool Exists(object primaryKey) {
            return DbSet.Find(primaryKey) != null;
        }

        /// <summary>
        /// Gets a single record
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public TEntity GetSingle(Func<TEntity, bool> predicate) {
            return DbSet.Single<TEntity>(predicate);
        }

        /// <summary>
        /// The first record matching the specified
        /// </summary>
        /// <param name="pridicate"></param>
        /// <returns></returns>
        public TEntity GetFirst(Func<TEntity, bool> pridicate) {

            return DbSet.First<TEntity>(pridicate);
        }


        #endregion
        

    }
}
