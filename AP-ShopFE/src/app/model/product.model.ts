import { Category } from "./category.model";
import { Condition } from "./condition.model";
import { Shipping } from "./shipping.model";

export class Product {
    id: number;
    productName: string;
    imagePath: string;
    price: number;
    description: string;
    isDeleted: boolean;
    quantity: number;
    category: Category;
    condition: Condition;
    shipping: Shipping;
    isModified: boolean;
}