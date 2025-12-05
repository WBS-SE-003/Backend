import { Router } from 'express';
import { createArticle, getAllArticles, getArticleById } from '#controllers';
import { payWall } from '#middlewares';

const articleRoutes = Router();

articleRoutes.get('/', getAllArticles);
articleRoutes.get('/:id', payWall, getArticleById);
articleRoutes.post('/', createArticle);

export default articleRoutes;
