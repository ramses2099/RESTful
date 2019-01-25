using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using DataModel.GenericRepository;
using System.Data.Entity.Validation;

namespace DataModel.UnitOfWork
{
    public class UnitOfWork : IDisposable
    {
        #region Private member variables

        private OnLineStoreEntities _context = null;
        private GenericRepository<Product> _productRepository = null;
        private bool disposed = false;


        #endregion

        public UnitOfWork()
        {
            _context = new OnLineStoreEntities();
        }
        
        #region Public Repository Creation properties

        /// <summary>
        /// Get/Set Property for procuct repository
        /// </summary>
        public GenericRepository<Product> ProductRepository
        {

            get
            {
                if (this._productRepository == null)
                    this._productRepository = new GenericRepository<Product>(_context);
                return _productRepository;

            }
        }



        #endregion

        #region Public member methods

        public void Save()
        {

            try
            {
                _context.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                var outputLines = new List<string>();
                foreach (var eve in e.EntityValidationErrors)
                {
                    outputLines.Add(string.Format(
                        "{0}: Entity of type \"{1}\" in state \"{2}\" has the following validation errors:", DateTime.Now,
                        eve.Entry.Entity.GetType().Name, eve.Entry.State));
                    foreach (var ve in eve.ValidationErrors)
                    {
                        outputLines.Add(string.Format("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage));
                    }
                }
                System.IO.File.AppendAllLines(@"C:\errors.txt", outputLines);

                throw e;

            }


        }

        #endregion
        
        /// <summary>
        /// Protected Virtual Dispose method
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {

            if (!this.disposed)
            {
                if (disposing)
                {
                    Debug.WriteLine("UnitOfWork is being disposed");
                    _context.Dispose();
                }

            }
            this.disposed = true;
        }

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {

            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}
