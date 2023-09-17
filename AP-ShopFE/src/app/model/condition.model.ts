export class Condition {
    public id: number;
    public conditionType: string;

    constructor(id: number, conditionType: string) {
        this.id = id;
        this.conditionType = conditionType;
    }
}