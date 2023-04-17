import { QuestItemGetResponse } from "./quest-item-get-response";

export class QuestGetResponse {
    public id: string = null!;

    public title: string = null!;

    public createTime: Date = null!;
    
    public updateTime: Date = null!;
    
    public approvedTime: Date | null = null;

    public questItems: QuestItemGetResponse[] = null!;
}
