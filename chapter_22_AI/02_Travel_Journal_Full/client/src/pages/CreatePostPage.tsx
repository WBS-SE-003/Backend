import { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import api from '@utils/api';
import Swal from 'sweetalert2';
import { BeatLoader } from 'react-spinners';

const CreatePostPage = () => {
  const navigate = useNavigate();
  const [isSubmitting, setIsSubmitting] = useState(false);
  const [fields, setFields] = useState({
    title: '',
    content: '',
  });
  const [imageFile, setImageFile] = useState<File | null>(null);

  const handleChange = (
    e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>
  ) => {
    setFields({ ...fields, [e.target.name]: e.target.value });
  };

  const handleFileChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    if (e.target.files && e.target.files[0]) {
      setImageFile(e.target.files[0]);
    } else {
      setImageFile(null);
    }
  };

  const handleSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    if (!fields.title.trim()) {
      Swal.fire({ icon: 'error', title: 'Title is required' });
      return;
    }
    setIsSubmitting(true);
    try {
      const formData = new FormData();
      formData.append('title', fields.title);
      if (fields.content) formData.append('content', fields.content);
      if (imageFile) formData.append('image', imageFile);

      await api.post('/posts', formData, {
        headers: { 'Content-Type': 'multipart/form-data' },
      });
      Swal.fire({
        icon: 'success',
        title: 'Post created!',
        text: 'Your post has been created successfully.',
      }).then(() => navigate('/'));
    } catch (err: unknown) {
      const error = err as { response?: { data?: { message?: string } } };
      Swal.fire({
        icon: 'error',
        title: 'Failed to create post',
        text: error?.response?.data?.message || 'Something went wrong',
      });
    } finally {
      setIsSubmitting(false);
    }
  };

  return (
    <div className='flex justify-center items-center min-h-screen bg-base-200 px-2 py-8'>
      <form
        onSubmit={handleSubmit}
        className='w-full max-w-lg bg-base-100 rounded-2xl shadow-2xl p-6 sm:p-10 space-y-6 border border-base-300'
      >
        <h2 className='text-3xl font-bold text-center mb-4'>
          Create a New Post
        </h2>
        <div className='alert alert-info mb-2'>
          💡If you leave the description or image fields empty, our AI will
          generate them for you based on your title.
        </div>
        <div className='grid gap-5'>
          <div className='grid grid-cols-1 sm:grid-cols-4 items-center gap-2'>
            <label className='font-semibold sm:text-right sm:pr-4 col-span-1 sm:col-span-1'>
              Title <span className='text-error'>*</span>
            </label>
            <div className='col-span-1 sm:col-span-3'>
              <input
                type='text'
                name='title'
                className='input input-bordered input-lg w-full'
                value={fields.title}
                onChange={handleChange}
                required
              />
            </div>
          </div>
          <div className='grid grid-cols-1 sm:grid-cols-4 items-center gap-2'>
            <label className='font-semibold sm:text-right sm:pr-4 col-span-1 sm:col-span-1'>
              Content
            </label>
            <div className='col-span-1 sm:col-span-3'>
              <textarea
                name='content'
                className='textarea textarea-bordered min-h-[120px] resize-none w-full'
                value={fields.content}
                onChange={handleChange}
                placeholder='AI will generate if left empty'
              />
            </div>
          </div>
          <div className='grid grid-cols-1 sm:grid-cols-4 items-center gap-2'>
            <label className='font-semibold sm:text-right sm:pr-4 col-span-1 sm:col-span-1'>
              Image File
            </label>
            <div className='col-span-1 sm:col-span-3'>
              <input
                type='file'
                name='image'
                accept='image/*'
                className='file-input file-input-bordered w-full'
                onChange={handleFileChange}
              />
              <span className='text-xs text-gray-500'>
                AI will generate if left empty
              </span>
            </div>
          </div>
        </div>
        <button
          type='submit'
          className='btn btn-primary text-white w-full btn-lg mt-2 shadow-md transition-opacity duration-200 disabled:!opacity-50 disabled:!bg-primary'
          disabled={isSubmitting}
        >
          {isSubmitting ? (
            <span className='flex items-center justify-center gap-2'>
              <BeatLoader color='#ffffff' />
            </span>
          ) : (
            'Create Post'
          )}
        </button>
      </form>
    </div>
  );
};

export default CreatePostPage;
