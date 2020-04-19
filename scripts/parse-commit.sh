IFS='|' read -ra my_array <<< $COMMIT
echo "::set-env name=COMMIT_ARRAY::$my_array"
