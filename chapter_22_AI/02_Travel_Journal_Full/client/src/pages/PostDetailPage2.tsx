import { useEffect, useState, useRef } from 'react';
import { useParams, Link } from 'react-router-dom';
import api from '@utils/api';
import Swal from 'sweetalert2';
import { format } from 'date-fns';
import type { Post } from 'types/post';
import { BeatLoader } from 'react-spinners';

type ChatMessage = { role: 'user' | 'ai'; content: string };

const FALLBACK_IMG =
  'https://upload.wikimedia.org/wikipedia/commons/1/14/No_Image_Available.jpg';

const PostDetailPage2 = () => {
  const { id } = useParams();
  const [post, setPost] = useState<Post | null>(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState('');
  const [chat, setChat] = useState<ChatMessage[]>([]);
  const [prompt, setPrompt] = useState('');
  const [aiLoading, setAiLoading] = useState(false);

  useEffect(() => {
    const fetchPost = async () => {
      setLoading(true);
      setError('');
      try {
        const res = await api.get(`/posts/${id}`);
        setPost(res.data);
      } catch {
        setError('Failed to load post');
        Swal.fire({
          icon: 'error',
          title: 'Oops...',
          text: 'Failed to load post',
        });
      } finally {
        setLoading(false);
      }
    };
    fetchPost();
  }, [id]);

  const displayName = post?.author?.firstName || 'Deleted User';

  const formatDate = (dateStr?: string) =>
    dateStr ? format(new Date(dateStr), 'dd.MM.yyyy · HH:mm') : '';

  const chatEndRef = useRef<HTMLDivElement>(null);
  const inputRef = useRef<HTMLInputElement>(null);

  const focusInput = () =>
    requestAnimationFrame(() =>
      inputRef.current?.focus({ preventScroll: true })
    );

  useEffect(() => {
    chatEndRef.current?.scrollIntoView({ behavior: 'smooth' });
  }, [chat]);

  async function handleAsk() {
    if (!prompt.trim() || !post?._id) return;
    const userText = prompt.trim();

    setAiLoading(true);
    setChat((prev) => [...prev, { role: 'user', content: userText }]);
    setPrompt('');
    focusInput();

    try {
      const res = await api.post('/ai/text', {
        prompt: userText,
        postId: post._id,
      });
      const answer = res.data?.choices?.[0]?.message?.content || 'No answer.';
      setChat((prev) => [...prev, { role: 'ai', content: answer }]);
    } catch {
      setChat((prev) => [
        ...prev,
        { role: 'ai', content: 'Failed to get AI response.' },
      ]);
    } finally {
      setAiLoading(false);
      focusInput();
    }
  }

  if (loading)
    return (
      <div className='flex justify-center items-center h-40'>
        <BeatLoader color='#ffffff' />
      </div>
    );

  if (error || !post)
    return <div className='text-center text-gray-500'>Post not found.</div>;

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
            onError={(e) => (e.currentTarget.src = FALLBACK_IMG)}
          />
        </figure>

        <div className='card-body'>
          <h2 className='card-title text-2xl mb-2'>{post.title}</h2>

          <div className='flex items-center gap-2 mb-2'>
            <span className='font-semibold text-primary hover:opacity-50 cursor-pointer'>
              {displayName}
            </span>
            <span className='text-gray-400'>{formatDate(post.createdAt)}</span>
          </div>

          <p className='mb-2 whitespace-pre-line'>{post.content}</p>

          {/* AI Chat Section */}
          <div className='mt-6 border-t pt-4'>
            <h3 className='font-bold mb-2 text-lg'>AI Chat about this post</h3>

            <div className='h-56 overflow-y-auto bg-base-200 rounded-lg p-3 mb-3 flex flex-col gap-2'>
              {chat.length === 0 && (
                <div className='text-gray-400 text-center'>
                  Ask anything about this post!
                </div>
              )}

              {chat.map((msg, i) => (
                <div
                  key={i}
                  className={`chat ${
                    msg.role === 'user' ? 'chat-end' : 'chat-start'
                  }`}
                >
                  <div
                    className={`chat-bubble ${
                      msg.role === 'user'
                        ? 'chat-bubble-primary text-white'
                        : 'chat-bubble-info'
                    }`}
                  >
                    {msg.content}
                  </div>
                </div>
              ))}

              {aiLoading && (
                <div className='chat chat-start'>
                  <div className='chat-bubble chat-bubble-info flex items-center'>
                    <BeatLoader color='#ffffff' />
                  </div>
                </div>
              )}

              <div ref={chatEndRef} />
            </div>

            <div className='flex gap-2'>
              <input
                ref={inputRef}
                type='text'
                className='input input-bordered flex-1'
                placeholder='Ask something about this post...'
                value={prompt}
                onChange={(e) => setPrompt(e.target.value)}
                onKeyDown={(e) => {
                  if (e.key === 'Enter') handleAsk();
                }}
              />
              <button
                className='btn btn-primary text-white'
                onMouseDown={(e) => e.preventDefault()}
                onClick={handleAsk}
                disabled={!prompt.trim()}
              >
                Send
              </button>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};

export default PostDetailPage2;
