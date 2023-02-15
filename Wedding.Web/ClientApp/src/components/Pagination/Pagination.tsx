import React, { useEffect, useState } from 'react';
import ReactDOM from 'react-dom';
import ReactPaginate from 'react-paginate';

type PaginatedItemsPropsType = {
    itemsPerPage: number
    items: number[]
    onChangePage: (page: number) => void
}

export const PaginatedItems = (props: PaginatedItemsPropsType) => {

    const pageCount = Math.ceil(props.items.length / props.itemsPerPage);
    const handlePageClick = (event: { selected: number }) => {
        props.onChangePage(event.selected)
    };

    return (
        <ReactPaginate
            nextLabel=">"
            onPageChange={handlePageClick}
            pageRangeDisplayed={3}
            marginPagesDisplayed={2}
            pageCount={pageCount}
            previousLabel="<"
            pageClassName="page-item"
            pageLinkClassName="page-link"
            previousClassName="page-item"
            previousLinkClassName="page-link"
            nextClassName="page-item"
            nextLinkClassName="page-link"
            breakLabel="..."
            breakClassName="page-item"
            breakLinkClassName="page-link"
            containerClassName="pagination"
            activeClassName="active"
            renderOnZeroPageCount={undefined}
        />
    );
}