import { useEffect, useState } from 'react';
import PostCard from '@components/PostCard';
import Spinner from '@components/Spinner';
import type { Post } from '../types/post';
import api from '@utils/api';

const HomePage = () => {
  const [posts, setPosts] = useState<Post[]>([]);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState('');

  useEffect(() => {
    const fetchPosts = async () => {
      setLoading(true);
      try {
        const res = await api.get('/posts');
        console.log(res);
        setPosts(res.data);
      } catch (err: unknown) {
        const error = err as { response?: { data?: { message?: string } } };
        // console.log(error);
        setError(error?.response?.data?.message || 'Failed to fetch posts');
      } finally {
        setLoading(false);
      }
    };

    fetchPosts();
  }, []);

  return (
    <div className='container mx-auto p-4'>
      {loading ? (
        <div className='flex justify-center items-center h-40'>
          <Spinner />
        </div>
      ) : error ? (
        <div className='text-center text-lg text-red-500'>{error}</div>
      ) : (
        <div className='grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 gap-8'>
          {posts.map((post) => (
            <PostCard key={post._id} {...post} />
          ))}
        </div>
      )}
    </div>
  );
};

export default HomePage;
