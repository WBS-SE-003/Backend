import { useState } from 'react';
import { useNavigate, Link } from 'react-router-dom';
import { useAuth } from '@context/AuthContext';
import Swal from 'sweetalert2';
import Spinner from '@components/Spinner';

const LoginPage = () => {
  const { login } = useAuth();
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [isSubmitting, setIsSubmitting] = useState(false);
  const navigate = useNavigate();

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setIsSubmitting(true);

    try {
      await login(email, password);
      navigate('/');
    } catch (err: unknown) {
      console.log(err);
      const error = err as { response?: { data?: { message?: string } } };
      Swal.fire({
        icon: 'error',
        title: 'Login failed',
        text: error?.response?.data?.message || 'Invalid creds',
      });
    } finally {
      setIsSubmitting(false);
    }
  };

  return (
    <div className='flex justify-center items-center min-h-[70vh]'>
      <form
        onSubmit={handleSubmit}
        className='card w-full max-w-sm bg-base-100 shadow-xl p-8 space-y-4'
      >
        <h2 className='text-2xl font-bold text-center mb-2'>Login</h2>
        <div className='form-control'>
          <label className='label'>Email</label>
          <input
            type='email'
            className='input input-bordered'
            value={email}
            onChange={(e) => setEmail(e.target.value)}
            required
          />
        </div>
        <div className='form-control'>
          <label className='label'>Password</label>
          <input
            type='password'
            className='input input-bordered w-full'
            value={password}
            onChange={(e) => setPassword(e.target.value)}
            required
          />
        </div>
        <button
          type='submit'
          className='btn btn-primary w-full mt-2 text-white'
          disabled={isSubmitting}
        >
          {isSubmitting ? <Spinner /> : 'Login'}
        </button>
        <div className='text-center mt-2'>
          <span className='text-sm'>Don't have an account? </span>
          <Link to='/signup' className='link text-primary'>
            Register
          </Link>
        </div>
      </form>
    </div>
  );
};

export default LoginPage;
