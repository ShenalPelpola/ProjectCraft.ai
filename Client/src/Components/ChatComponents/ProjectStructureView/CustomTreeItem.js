import { forwardRef } from 'react';
import clsx from 'clsx';
import { unstable_useTreeItem2 as useTreeItem2 } from '@mui/x-tree-view/useTreeItem2';
import { TreeItem2IconContainer } from '@mui/x-tree-view/TreeItem2';
import { TreeItem2Icon } from '@mui/x-tree-view/TreeItem2Icon';
import { TreeItem2Provider } from '@mui/x-tree-view/TreeItem2Provider';
import FolderRounded from '@mui/icons-material/FolderRounded';

export const CustomTreeItem = forwardRef(function CustomTreeItem(props, ref) {
    const { id, itemId, label, disabled, children, ...other } = props;
    const {
        getRootProps,
        getContentProps,
        getIconContainerProps,
        getLabelProps,
        getGroupTransitionProps,
        status,
        publicAPI,
    } = useTreeItem2({ id, itemId, children, label, disabled, rootRef: ref });

    const isExpandable = (reactChildren) => {
        if (Array.isArray(reactChildren)) {
            return reactChildren.length > 0 && reactChildren.some(isExpandable);
        }

        return Boolean(reactChildren);
    };

    const item = publicAPI.getItem(itemId);
    const expandable = isExpandable(children);
    let icon;

    if (expandable) {
        icon = FolderRounded;
    }
    else if (item.fileType) {
        icon = getIconFromFileType(item.fileType);
    }

    return (
        <TreeItem2Provider itemId={itemId}>
            <StyledTreeItemRoot {...getRootProps(other)}>
                <CustomTreeItemContent
                    {...getContentProps({
                        className: clsx('content', {
                            'Mui-expanded': status.expanded,
                            'Mui-selected': status.selected,
                            'Mui-focused': status.focused,
                            'Mui-disabled': status.disabled,
                        }),
                    })}
                >
                    <TreeItem2IconContainer {...getIconContainerProps()}>
                        <TreeItem2Icon status={status} />
                    </TreeItem2IconContainer>

                    <CustomLabel
                        {...getLabelProps({ icon, expandable: expandable && status.expanded })}
                    />
                </CustomTreeItemContent>
                {children && <TransitionComponent {...getGroupTransitionProps()} />}
            </StyledTreeItemRoot>
        </TreeItem2Provider>
    );
});