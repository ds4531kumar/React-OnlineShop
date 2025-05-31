import { Grid } from "@mui/material";
import { Product } from "../../app/model/Product";
import ProductCard from "./ProductCard";

type ProductListProps = {
  products: Product[];
};
export default function ProductList({ products }: ProductListProps) {
  return (
    <Grid container spacing={3}>
      {products.map((product) => (
        <Grid size={3} display="flex" key={product.id}>
          <ProductCard product={product}></ProductCard>
        </Grid>
      ))}
    </Grid>
  );
}
