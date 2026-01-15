import multer from 'multer';
import { v2 as cloudinary } from 'cloudinary';
import { CloudinaryStorage } from 'multer-storage-cloudinary';
import { format } from 'path';

cloudinary.config({
  cloud_name: process.env.CLOUD_NAME,
  api_key: process.env.API_KEY,
  api_secret: process.env.API_SECRET,
});

const storage = new CloudinaryStorage({
  cloudinary,
  params: {
    folder: 'posts',
    format: 'webp',
    public_id: () => `blogpost-${Date.now()}`,
    resource_type: 'image',
    transformation: [{ width: 1920, height: 1080, crop: 'limit' }],
    allowedFormats: ['jpeg', 'jpg', 'png', 'webp', 'gif'],

    // limits: {
    // fileSize: 2 * 1024 * 1024, // 2MB
    // fileSize: 8 * 1024 * 1024  // = 8 MB
    // },
  } as any,
});

const upload = multer({ storage });

export default upload;
