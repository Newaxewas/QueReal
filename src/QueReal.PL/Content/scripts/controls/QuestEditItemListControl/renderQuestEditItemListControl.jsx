import * as ReactDom from "react-dom/client";
import { QuestEditItemListControl } from "./QuestEditItemListControl.jsx";

export function renderQuestEditItemListControl(rootId, items) {
    const root = ReactDom.createRoot(document.getElementById(rootId));

    root.render(
        <QuestEditItemListControl items={items}/>
    );
}
