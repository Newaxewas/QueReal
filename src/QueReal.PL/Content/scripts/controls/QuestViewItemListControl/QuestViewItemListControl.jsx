import { useEffect, useState } from "react";
import { QuestViewItemControl } from "./QuestViewItemControl.jsx";

export function QuestViewItemListControl(props) {
    const [items, setItems] = useState([]);

    useEffect(() => setItems(props.items), []);

    const onChangeItem = (index, value) => setItems(items.map((element, i) => index !== i ? element : Object.assign({}, element, {progress: value})));

    return (
        props.items.map(item => <QuestViewItemControl value={item} key={item.id} onChange={onChangeItem}/>)
    )
}
