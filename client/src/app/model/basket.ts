import { Product } from "./Product";

export type Basket = {
    baskedId: string;
    items: Item[];
    clientSecret?: string;
    paymentIntentId?: string;
};

export class Item {
    constructor(public product: Product, quantity: number) {
        this.productId = product.id;
        this.name = product.name;
        this.quantity = quantity;
        this.price = product.price;
        this.pictureUrl = product.pictureUrl;
        this.brand = product.brand;
        this.type = product.type;
    }
    productId: number;
    quantity: number;
    price: number;
    name: string;
    pictureUrl: string;
    brand: string;
    type: string;
}
