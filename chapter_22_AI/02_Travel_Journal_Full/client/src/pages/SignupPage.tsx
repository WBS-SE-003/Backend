import { useState } from 'react';
import { useNavigate, Link } from 'react-router-dom';
import { useAuth } from '@context/AuthContext';
import Swal from 'sweetalert2';

const SignupPage = () => {
  const { register } = useAuth();
  const navigate = useNavigate();
  const [fields, setFields] = useState({
    firstName: '',
    lastName: '',
    email: '',
    password: '',
    confirmPassword: '',
  });

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setFields({ ...fields, [e.target.name]: e.target.value });
  };

  const handleSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    if (fields.password !== fields.confirmPassword) {
      Swal.fire({
        icon: 'error',
        title: 'Passwords do not match',
      });
      return;
    }
    try {
      await register(
        fields.firstName,
        fields.lastName,
        fields.email,
        fields.password
      );
      Swal.fire({
        icon: 'success',
        title: 'Registration successful!',
        html: `You can now <a href='/login' class='text-primary underline'>login</a>.`,
        showConfirmButton: true,
        confirmButtonText: 'Go to Login',
      }).then(() => {
        navigate('/login');
      });
    } catch (err: unknown) {
      const error = err as { response?: { data?: { message?: string } } };
      Swal.fire({
        icon: 'error',
        title: 'Registration failed',
        text: error?.response?.data?.message || 'Please check your details',
      });
    }
  };

  return (
    <div className='flex justify-center items-center min-h-[70vh]'>
      <form
        onSubmit={handleSubmit}
        className='card w-full max-w-sm bg-base-100 shadow-xl p-8 space-y-4'
      >
        <h2 className='text-2xl font-bold text-center mb-2'>Register</h2>
        <div className='form-control'>
          <label className='label'>
            First Name <span className='text-error'>*</span>
          </label>
          <input
            type='text'
            name='firstName'
            className='input input-bordered'
            value={fields.firstName}
            onChange={handleChange}
            required
          />
        </div>
        <div className='form-control'>
          <label className='label'>
            Last Name <span className='text-error'>*</span>
          </label>
          <input
            type='text'
            name='lastName'
            className='input input-bordered'
            value={fields.lastName}
            onChange={handleChange}
            required
          />
        </div>
        <div className='form-control'>
          <label className='label'>
            Email <span className='text-error'>*</span>
          </label>
          <input
            type='email'
            name='email'
            className='input input-bordered'
            value={fields.email}
            onChange={handleChange}
            required
          />
        </div>
        <div className='form-control'>
          <label className='label'>
            Password <span className='text-error'>*</span>
          </label>
          <input
            type='password'
            name='password'
            className='input input-bordered w-full'
            value={fields.password}
            onChange={handleChange}
            required
          />
        </div>
        <div className='form-control'>
          <label className='label'>
            Confirm Password <span className='text-error'>*</span>
          </label>
          <input
            type='password'
            name='confirmPassword'
            className='input input-bordered w-full'
            value={fields.confirmPassword}
            onChange={handleChange}
            required
          />
        </div>
        <button
          type='submit'
          className='btn btn-primary text-white w-full mt-2'
        >
          Register
        </button>
        <div className='text-center mt-2'>
          <span className='text-sm'>Already have an account? </span>
          <Link to='/login' className='link text-primary'>
            Login
          </Link>
        </div>
      </form>
    </div>
  );
};

export default SignupPage;
