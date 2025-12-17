import mongoose from 'mongoose';

const MONGO_URI = process.env.MONGO_URI!;

try {
  const client = await mongoose.connect(MONGO_URI);
  console.log('✔️  connected to MongoDB');
  console.log(`📦 using db: ${client.connection.name}`);
} catch (error) {
  console.error('❌ MongoDB connection error:', error);
  process.exit(1);
}
