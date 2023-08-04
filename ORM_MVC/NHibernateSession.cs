using NHibernate;
using NHibernate.Cfg;
using ISession = NHibernate.ISession;

namespace ORM_MVC
{
    public class NHibernateSession
    {
        public static ISession OpenSession()
        {
            var configuration = new Configuration();
            configuration.Configure(@"D:\ASP.net-2\ORM_MVC\ORM_MVC\Models\hibernate.cfg.xml");
            configuration.AddFile(@"D:\ASP.net-2\ORM_MVC\ORM_MVC\Mapping\Book.hbm.xml");
            ISessionFactory sessionFactory = configuration.BuildSessionFactory();
            return sessionFactory.OpenSession();
        }       
      

    }
}
