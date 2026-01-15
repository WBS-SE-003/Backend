import { useState, useEffect } from 'react';
import { useNavigate, useParams, Link } from 'react-router-dom';
import { useAuth } from '@context/AuthContext';
import Spinner from '@components/Spinner';
import api from '@utils/api';
import Swal from 'sweetalert2';
import { format } from 'date-fns';
import type { Post } from '../types/post';
// import axios from 'axios';

const PostDetailPage = () => {
  const { id } = useParams();
  const { user } = useAuth();
  const [post, setPost] = useState<Post | null>(null);
  const [editMode, setEditMode] = useState(false);
  const [editFields, setEditFields] = useState({ title: '', content: '' });
  const [isSaving, setIsSaving] = useState(false);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState('');
  const navigate = useNavigate();

  useEffect(() => {
    const fetchPost = async () => {
      setLoading(true);
      setError('');
      try {
        const res = await api.get(`/posts/${id}`);

        setPost(res.data);
      } catch (err) {
        setError('Failed to load post');
      } finally {
        setLoading(false);
      }
    };
    fetchPost();
  }, [id]);

  const canEditOrDelete =
    user &&
    post &&
    (user._id === post.author._id || user.roles.includes('admin'));

  const handleEditClick = () => {
    if (!post) return;
    setEditFields({ title: post.title, content: post.content });
    setEditMode(true);
  };

  const handleEditChange = (
    e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>
  ) => {
    setEditFields({ ...editFields, [e.target.name]: e.target.value });
  };

  const handleEditCancel = () => {
    setEditMode(false);
  };

  const handleEditSave = async () => {
    if (!post) return;
    setIsSaving(true);
    try {
      await api.put(`/posts/${post._id}`, {
        title: editFields.title,
        content: editFields.content,
        image: post.image,
      });
      setPost({
        ...post,
        title: editFields.title,
        content: editFields.content,
      });
      setEditMode(false);
      Swal.fire({ icon: 'success', title: 'Post updated!' });
    } catch (err: unknown) {
      const error = err as { response?: { data?: { message?: string } } };
      Swal.fire({
        icon: 'error',
        title: 'Failed to update',
        text: error?.response?.data?.message || 'Something went wrong',
      });
    } finally {
      setIsSaving(false);
    }
  };

  const handleDelete = async () => {
    const result = await Swal.fire({
      title: 'Are you sure?',
      text: 'This action cannot be undone!',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonText: 'Yes, delete it!',
      cancelButtonText: 'Cancel',
    });
    if (result.isConfirmed) {
      try {
        await api.delete(`/posts/${id}`);
        Swal.fire('Deleted!', 'The post has been deleted.', 'success');
        navigate('/');
      } catch (err: unknown) {
        const error = err as { response?: { data?: { message?: string } } };
        Swal.fire({
          icon: 'error',
          title: 'Failed to delete',
          text: error?.response?.data?.message || 'Something went wrong',
        });
      }
    }
  };

  if (loading)
    return (
      <div className='flex justify-center items-center h-40'>
        <Spinner />
      </div>
    );
  if (error || !post)
    return (
      <div className='text-center text-gray-500'>
        <div>Post not found.</div>
        <div className='mt-4'>
          <Link to='/' className='btn btn-primary btn-lg px-8 text-white'>
            &larr; Home
          </Link>
        </div>
      </div>
    );

  // format date as dd.MM.yyyy · HH:mm (24h) ==> date-fns package: https://date-fns.org/
  const formatDate = (dateStr?: string) => {
    if (!dateStr) return '';
    return format(new Date(dateStr), 'dd.MM.yyyy · HH:mm');
  };

  const displayName = post.author?.firstName || 'Deleted User';

  return (
    <div className='container mx-auto p-4 max-w-2xl'>
      <Link to='/' className='btn btn-ghost mb-4'>
        &larr; Back
      </Link>
      <div className='card bg-base-100 shadow-xl'>
        <figure>
          <img
            src={post.image}
            alt={post.title}
            className='w-full object-cover rounded-t-xl'
            onError={(e) =>
              (e.currentTarget.src =
                'https://upload.wikimedia.org/wikipedia/commons/1/14/No_Image_Available.jpg')
            }
          />
        </figure>
        <div className='card-body'>
          <div className='flex justify-between items-start mb-2'>
            {editMode ? (
              <input
                type='text'
                name='title'
                className='input input-bordered input-lg flex-1 mr-2'
                value={editFields.title}
                onChange={handleEditChange}
                disabled={isSaving}
              />
            ) : (
              <h2 className='card-title text-2xl'>{post.title}</h2>
            )}
            {canEditOrDelete && !editMode && (
              <div className='flex gap-2'>
                <button
                  className='btn btn-sm btn-outline btn-info'
                  onClick={handleEditClick}
                >
                  Edit
                </button>
                <button
                  className='btn btn-sm btn-outline btn-error'
                  onClick={handleDelete}
                >
                  Delete
                </button>
              </div>
            )}
          </div>
          <div className='flex items-center gap-2 mb-2'>
            <span className='font-semibold text-primary hover:opacity-50 cursor-pointer'>
              {displayName}
            </span>
            <span className='text-gray-400'>{formatDate(post.createdAt)}</span>
          </div>
          {editMode ? (
            <textarea
              name='content'
              className='textarea textarea-bordered min-h-[120px] w-full mb-2'
              value={editFields.content}
              onChange={handleEditChange}
              disabled={isSaving}
            />
          ) : (
            <p className='mb-2 whitespace-pre-line'>{post.content}</p>
          )}
          {editMode && (
            <div className='flex gap-2 mt-2'>
              <button
                className='btn btn-primary btn-sm text-white'
                onClick={handleEditSave}
                disabled={isSaving}
              >
                {isSaving ? 'Saving...' : 'Save'}
              </button>
              <button
                className='btn btn-outline btn-sm'
                onClick={handleEditCancel}
                disabled={isSaving}
              >
                Cancel
              </button>
            </div>
          )}
        </div>
      </div>
    </div>
  );
};

export default PostDetailPage;
