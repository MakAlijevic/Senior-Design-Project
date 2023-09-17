export class Shipping {
    public id: number;
    public shippingType: string;

    constructor(id: number, shippingType: string) {
        this.id = id;
        this.shippingType = shippingType;
    }
}