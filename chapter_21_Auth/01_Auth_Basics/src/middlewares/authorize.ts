import { Post } from '#models';
import type { RequestHandler } from 'express';

const authorize: RequestHandler = async (req, res, next) => {
  const { id } = req.params;
  const post = await Post.findById(id);

  console.log(req.user);

  if (!post) {
    return next(new Error('Post not found', { cause: { status: 404 } }));
  }

  if (req.user?.roles?.includes('admin')) {
    return next();
  }

  if (post.author.toString() !== req.user?.id) {
    return next(
      new Error('Forbidden: You cannot modify this post', {
        cause: { status: 403 },
      })
    );
  }

  next();
};

export default authorize;
