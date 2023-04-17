import { QuestGetResponse } from "./quest-get-response";

export class QuestGetAllResponse {
    public totalItemCount: number = null!;

    public quests: QuestGetResponse[] = null!;
}
