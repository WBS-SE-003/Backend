import { Link } from 'react-router-dom';
import { format } from 'date-fns';
import type { Post } from '../types/post';
export type PostCardProps = Post;

const PostCard = ({
  _id,
  title,
  image,
  content,
  author,
  createdAt,
}: PostCardProps) => {
  // format date as dd.MM.yyyy · HH:mm (24h) ==> date-fns package: https://date-fns.org/
  const formatDate = (dateStr?: string) => {
    if (!dateStr) return '';
    return format(new Date(dateStr), 'dd.MM.yyyy · HH:mm');
  };

  const displayName = author?.firstName || 'Deleted User';

  return (
    <div className='card w-full shadow-xl hover:shadow-2xl'>
      <Link to={`/post/${_id}`}>
        <figure className='w-full overflow-hidden rounded-t-xl'>
          <img
            src={image}
            alt={title}
            className='w-full h-full object-cover bg-base-200'
            onError={(e) =>
              (e.currentTarget.src =
                'https://upload.wikimedia.org/wikipedia/commons/1/14/No_Image_Available.jpg')
            }
          />
        </figure>
      </Link>

      <div className='card-body'>
        <h3 className='font-bold text-2xl text-center font-serif text-orange-400'>
          {title}
        </h3>
        <div className='flex items-center justify-between'>
          <span className='font-semibold text-primary'>{displayName}</span>
          <span className='text-xs text-gray-400'>{formatDate(createdAt)}</span>
        </div>

        <p>
          {content.length > 100 ? `${content.slice(0, 100)}...` : content}
          {content.length > 100 && (
            <Link
              to={`/post/${_id}`}
              className='link ml-1 text-blue-500 hover:opacity-50'
            >
              read more
            </Link>
          )}
        </p>
      </div>
    </div>
  );
};

export default PostCard;
