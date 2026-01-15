import { Routes, Route } from 'react-router-dom';
import Navbar from '@components/Navbar';
import HomePage from '@pages/HomePage';
import SignupPage from '@pages/SignupPage';
import LoginPage from '@pages/LoginPage';
import CreatePostPage from '@pages/CreatePostPage';
import PostDetailPage from '@pages/PostDetailPage';
import NotFoundPage from '@pages/NotFoundPage';
import PostDetailPage2 from '@pages/PostDetailPage2';

function App() {
  return (
    <div data-theme='light' className='min-h-screen'>
      <Navbar />
      <Routes>
        <Route path='/' element={<HomePage />} />
        <Route path='/post/:id' element={<PostDetailPage />} />
        <Route path='/post/new' element={<CreatePostPage />} />
        <Route path='/signup' element={<SignupPage />} />
        <Route path='/login' element={<LoginPage />} />
        <Route path='*' element={<NotFoundPage />} />
      </Routes>
    </div>
  );
}

export default App;
