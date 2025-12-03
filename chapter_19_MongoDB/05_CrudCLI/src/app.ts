import '#db';
import { Product } from '#models';
import { Command } from 'commander';

const program = new Command();

program
  .name('eCommerce CLI')
  .description('Simple CRUD Operations')
  .version('1.0.0');

// CREATE
program
  .command('add')
  .description('add a new product')
  .argument('<name>', 'Product Name')
  .argument('<stock>', 'Stock quantity')
  .argument('<price>', 'Product price')
  .action(async (name, quantity, unitPrice) => {
    const stock = Number(quantity);
    const price = Number(unitPrice);

    try {
      await Product.create({
        name,
        stock,
        price,
      });
      console.log(`✅ Product added: ${name} ${stock} pcs at $${price}`);
    } catch (error) {
      console.log('error adding product', error);
    }
  });

// READ
program
  .command('list')
  .description('List all products')
  .action(async () => {
    try {
      const products = await Product.find();

      if (products.length === 0) {
        console.log('📦 No products found');
        return;
      }

      console.log('📦 Products:');
      products.forEach((product, index) => {
        console.log(`
            ${index + 1}. ${product.name} - ${product.stock} pcs - $${
          product.price
        }`);
      });
    } catch (error) {
      console.log('error listing products', error);
    }
  });

// UPDATE
program
  .command('update')
  .description('update product by name')
  .argument('<oldName>', 'current product name')
  .argument('<newName>', 'new product name')
  .argument('<stock>', 'new stock quantity')
  .argument('<price>', 'new product price')
  .action(async (oldName, newName, quantity, unitPrice) => {
    const stock = Number(quantity);
    const price = Number(unitPrice);

    try {
      const product = await Product.findOneAndUpdate(
        { name: oldName },
        { name: newName, stock, price },
        { new: true }
      );

      if (product) {
        console.log(
          `🔃 Updated: ${oldName} => ${newName} ${stock} pcs at $${price}`
        );
      } else {
        console.log('Product not found');
      }
    } catch (error) {
      console.error('error updating product', error);
    }
  });

program
  .command('delete')
  .description('delete product by name')
  .argument('<name>', 'product name')
  .action(async (name) => {
    try {
      const product = await Product.findOneAndDelete({ name });
      if (product) {
        console.log(`🗑️  Product deleted: ${name}`);
      } else {
        console.log('product not found');
      }
    } catch (error) {
      console.error('error deleting product', error);
    }
  });

program
  .command('clearDB')
  .description('clear database')
  .action(async () => {
    try {
      const result = await Product.deleteMany({});
      console.log(result);
    } catch (error) {
      console.log('error clearing db', error);
    }
  });

program.hook('postAction', () => process.exit(0));
program.parse();
