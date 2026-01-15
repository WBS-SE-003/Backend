import { Link } from 'react-router-dom';

const NotFoundPage = () => (
  <div className='flex flex-col items-center justify-center min-h-[60vh] text-center'>
    <h1 className='text-7xl font-bold text-error mb-4'>404</h1>
    <h2 className='text-4xl font-semibold mb-2'>Page Not Found</h2>
    <p className='mb-6 text-gray-500'>
      Sorry, the page you are looking for does not exist.
    </p>
    <Link to='/' className='btn btn-primary btn-lg px-8 text-white'>
      &larr; Home
    </Link>
  </div>
);

export default NotFoundPage;
