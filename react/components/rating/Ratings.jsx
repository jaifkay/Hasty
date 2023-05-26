import React, { useState, useEffect } from 'react';
import { Rating } from 'react-simple-star-rating';
import * as ratingsService from '../../services/ratingsService';
import Swal from 'sweetalert2';
import PropTypes from 'prop-types';

import debug from 'sabio-debug';
const _logger = debug.extend('RatingStars');

function Ratings(props) {
  const [entityInfo] = useState({
    entityId: props.entityId,
    entityTypeId: props.entityTypeId,
  });

  const [ratingData, setRatingData] = useState({
    average: 0,
    totalCount: 0,
    ratingVal: 0,
    commentId: null,
  });

  useEffect(() => {
    ratingsService
      .getRatingSummary(entityInfo.entityId, entityInfo.entityTypeId)
      .then(onRatingDataSuccess)
      .catch(onRatingDataError);
  }, []);

  const onRatingDataSuccess = (response) => {
    _logger(response);
    const rating = response.item;
    setRatingData((prevState) => {
      const newRatingData = { ...prevState };
      newRatingData.average = rating.average;
      newRatingData.totalCount = rating.totalCount;
      return newRatingData;
    });
  };

  const onRatingDataError = (err) => {
    _logger(err);
  };

  const onStarClicked = (rate) => {
    setRatingData((prevState) => {
      let newRatingClicked = { ...prevState };
      newRatingClicked.ratingVal = rate;
      return newRatingClicked;
    });
  };

  useEffect(() => {
    let payload = { ...ratingData };

    payload.entityId = props.entityId;
    payload.entityTypeId = props.entityTypeId;

    if (ratingData.ratingVal > 0) {
      ratingsService.addRating(payload).then(onAddRatingSuccess).catch(onAddRatingError);
    } else {
    }
  }, [ratingData.ratingVal]);

  const onAddRatingSuccess = () => {
    setRatingData((prevState) => {
      let ns = { ...prevState };
      ns.totalCount += 1;
      return ns;
    });
    Swal.fire('Rating submitted', 'Thanks for taking your time to rate our product!', 'success');
  };

  const onAddRatingError = () => {
    Swal.fire({
      icon: 'error',
      title: 'Oh no...',
      text: 'Rating was unsuccessful. Please try again',
    });
  };

  const handleTotalIncrement = () => {
    setRatingData((prevState) => {
      let ns = { ...prevState };
      ns.totalCount += 1;
      return ns;
    });
  };

  return (
    <>
      <Rating
        readonly={props.isReadOnly}
        size={26}
        allowFraction={true}
        onClick={onStarClicked}
        initialValue={ratingData.average}
        increment={handleTotalIncrement}
      />{' '}
      ({ratingData.totalCount})
    </>
  );
}

Ratings.propTypes = {
  entityId: PropTypes.number.isRequired,
  entityTypeId: PropTypes.number.isRequired,
  isReadOnly: PropTypes.bool.isRequired,
};
export default Ratings;
