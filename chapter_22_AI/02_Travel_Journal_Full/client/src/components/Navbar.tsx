import { useAuth } from '@context/AuthContext';
import { Link } from 'react-router-dom';

const Navbar = () => {
  const { user, logout, loading } = useAuth();

  return (
    <nav className='navbar shadow-lg px-4 mb-6'>
      <div className='flex-1'>
        <Link
          to='/'
          className='font-semibold text-2xl cursor-pointer hover:opacity-50'
        >
          🧳SE#001 <span className='text-primary'>TravelJournal</span>
        </Link>
      </div>

      <div className='flex-none gap-4 items-center'>
        {!loading && user ? (
          <div className='flex items-center gap-3'>
            <span className='font-medium text-base'>
              Welcome back,{' '}
              <span className='text-primary'>{user.firstName}</span>
            </span>
            <Link to='/post/new' className='btn btn-primary btn-md'>
              Post
            </Link>
            <button onClick={logout} className='btn btn-outline btn-md'>
              Logout
            </button>
          </div>
        ) : (
          <Link to='/login' className='btn btn-primary btn-md'>
            Login
          </Link>
        )}
      </div>
    </nav>
  );
};

export default Navbar;
