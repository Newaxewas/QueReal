import { useEffect, useState } from "react";
import { QuestViewItemControl } from "./QuestViewItemControl.jsx";

export function QuestViewItemListControl(props) {
    const [items, setItems] = useState([]);

    useEffect(() => setItems(props.items), []);

    const onChangeItem = (index, value) => setItems(items.map((element, i) => index !== i ? element : Object.assign({}, element, {progress: value})));

    return (
        items.map((item,index) => <QuestViewItemControl value={item} index={index} key={item.id} onChange={onChangeItem}/>)
    )
}
