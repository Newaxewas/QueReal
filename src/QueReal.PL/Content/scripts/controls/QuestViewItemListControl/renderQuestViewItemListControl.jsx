import * as ReactDom from "react-dom/client";
import { QuestViewItemListControl } from "./QuestViewItemListControl.jsx";

export function renderQuestViewItemListControl(rootId, items) {
    const root = ReactDom.createRoot(document.getElementById(rootId));

    root.render(
        <QuestViewItemListControl items={items}/>
    );
}
