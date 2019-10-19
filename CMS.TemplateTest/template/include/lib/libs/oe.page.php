<?php
/*-----------------------------------
 * @version: OEcms v2.0 Free(免费版)
 * @website: www.phpoe.com,www.phpcoo.com
 * @email  : phpzac@foxmail.com
 * @qq     : 944811833,16100225
 * @update : 2011.02.20
-----------------------------------*/

/**
	前端文章分页二 样式 首页 上一页 下一页 末页
	@param $cateid -- 当前分类ID
	@param $pagecount -- 分页总数
	@param $page -- 当前页
	@param $pagesize -- 每页显示数量
	@param $mpurl -- URL地址
*/
function article_showpage($cateid,$pagecount,$page,$pagesize,$mpurl){   
    $multipage ="";
	$tipage = "";
    $mpurl .= strpos($mpurl, '?') ? '&amp;' : '?';
	if($pagecount>1){
		if($page==1 || $page<1){
			$tipage = "首页&nbsp;&nbsp;上一页&nbsp;&nbsp;<a href='".$mpurl."page=".($page+1)."'>下一页</a>&nbsp;&nbsp;<a href='".$mpurl."page=".$pagecount."'>末页</a>";
		}else{
			if($page == $pagecount){
				$tipage = "<a href='".$mpurl."page=1'>首页</a>&nbsp;&nbsp;<a href='".$mpurl."page=".($page-1)."'>上一页</a>&nbsp;&nbsp;下一页&nbsp;&nbsp;末页";
			}else{
				$tipage = "<a href='".$mpurl."page=1'>首页</a>&nbsp;&nbsp;<a href='".$mpurl."page=".($page-1)."'>上一页</a>&nbsp;&nbsp;<a href='".$mpurl."page=".($page+1)."'>下一页</a>&nbsp;&nbsp;<a href='".$mpurl."page=".$pagecount."'>末页</a>";
			}
		}
		$tipage.="&nbsp;&nbsp;(当前".$page."页/共".$pagecount."页)&nbsp;&nbsp;";
	}
	$multipage.=$tipage;
	return $multipage;
}

/**
	前端文章分页二 样式 首页 上一页 下一页 末页 伪静态
	@param $channel   -- 频道(用于Rewrite URL)
	@param $cateid    -- 当前分类ID
	@param $keyword   -- 产品关键字
	@param $pagecount -- 分页总数
	@param $page      -- 当前页
	@param $pagesize  -- 每页显示数量
	@param $mpurl     -- URL地址(非Rewrite URL)
*/
function url_showpage($channel,$cateid,$keyword,$pagecount,$page,$pagesize,$mpurl){   
    $multipage ="";
	$tipage = "";

    //URL路由处理
	if($GLOBALS['config']['htmltype']=="php"){
        $mpurl .= strpos($mpurl, '?') ? '&amp;' : '?';
		if($pagecount>1){
			if($page==1 || $page<1){
				$tipage = "首页&nbsp;&nbsp;上一页&nbsp;&nbsp;<a href='".$mpurl."page=".($page+1)."'>下一页</a>&nbsp;&nbsp;<a href='".$mpurl."page=".$pagecount."'>末页</a>";
			}else{
				if($page == $pagecount){
					$tipage = "<a href='".$mpurl."page=1'>首页</a>&nbsp;&nbsp;<a href='".$mpurl."page=".($page-1)."'>上一页</a>&nbsp;&nbsp;下一页&nbsp;&nbsp;末页";
				}else{
					$tipage = "<a href='".$mpurl."page=1'>首页</a>&nbsp;&nbsp;<a href='".$mpurl."page=".($page-1)."'>上一页</a>&nbsp;&nbsp;<a href='".$mpurl."page=".($page+1)."'>下一页</a>&nbsp;&nbsp;<a href='".$mpurl."page=".$pagecount."'>末页</a>";
				}
			}
			$tipage.="&nbsp;&nbsp;(当前".$page."页/共".$pagecount."页)&nbsp;&nbsp;";
		}
	}else{

		//伪静态
		if((int)$pagecount>1){
            
			
			if((int)$cateid>0){
                //有分类ID的
				if($page==1 || $page<1){
					$tipage = "首页&nbsp;&nbsp;上一页&nbsp;&nbsp;<a href='".$channel."-cat-".$cateid."-page-".($page+1).".html'>下一页</a>&nbsp;&nbsp;<a href='".$channel."-cat-".$cateid."-page-".$pagecount.".html'>末页</a>";
				}else{
					if($page == $pagecount){
						$tipage = "<a href='".$channel."-cat-".$cateid."-page-1.html'>首页</a>&nbsp;&nbsp;<a href='".$channel."-cat-".$cateid."-page-".($page-1).".html'>上一页</a>&nbsp;&nbsp;下一页&nbsp;&nbsp;末页";
					}else{
						$tipage = "<a href='".$channel."-cat-".$cateid."-page-1.html'>首页</a>&nbsp;&nbsp;<a href='".$channel."-cat-".$cateid."-page-".($page-1).".html'>上一页</a>&nbsp;&nbsp;<a href='".$channel."-cat-".$cateid."-page-".($page+1).".html'>下一页</a>&nbsp;&nbsp;<a href='".$channel."-cat-".$cateid."-page-".$pagecount.".html'>末页</a>";
					}
				}
				$tipage.="&nbsp;&nbsp;(当前".$page."页/共".$pagecount."页)&nbsp;&nbsp;";
			
			}else{
				//没有分类ID的
				if(ischar($keyword)){
					//关键字处理
					if($page==1 || $page<1){
						$tipage = "首页&nbsp;&nbsp;上一页&nbsp;&nbsp;<a href='".$channel."-k-".urlencode($keyword)."-page-".($page+1).".html'>下一页</a>&nbsp;&nbsp;<a href='".$channel."-k-".urlencode($keyword)."-page-".$pagecount.".html'>末页</a>";
					}else{
						if($page == $pagecount){
							$tipage = "<a href='".$channel."-k-".urlencode($keyword)."-page-1.html'>首页</a>&nbsp;&nbsp;<a href='".$channel."-k-".urlencode($keyword)."-page-".($page-1).".html'>上一页</a>&nbsp;&nbsp;下一页&nbsp;&nbsp;末页";
						}else{
							$tipage = "<a href='".$channel."-k-".urlencode($keyword)."-page-1.html'>首页</a>&nbsp;&nbsp;<a href='".$channel."-k-".urlencode($keyword)."-page-".($page-1).".html'>上一页</a>&nbsp;&nbsp;<a href='".$channel."-k-".urlencode($keyword)."-page-".($page+1).".html'>下一页</a>&nbsp;&nbsp;<a href='".$channel."-k-".urlencode($keyword)."-page-".$pagecount.".html'>末页</a>";
						}
					}
					$tipage.="&nbsp;&nbsp;(当前".$page."页/共".$pagecount."页)&nbsp;&nbsp;";

				}else{

					//关键字为空
					if($page==1 || $page<1){
						$tipage = "首页&nbsp;&nbsp;上一页&nbsp;&nbsp;<a href='".$channel."-page-".($page+1).".html'>下一页</a>&nbsp;&nbsp;<a href='".$channel."-page-".$pagecount.".html'>末页</a>";
					}else{
						if($page == $pagecount){
							$tipage = "<a href='".$channel."-page-1.html'>首页</a>&nbsp;&nbsp;<a href='".$channel."-page-".($page-1).".html'>上一页</a>&nbsp;&nbsp;下一页&nbsp;&nbsp;末页";
						}else{
							$tipage = "<a href='".$channel."-page-1.html'>首页</a>&nbsp;&nbsp;<a href='".$channel."-page-".($page-1).".html'>上一页</a>&nbsp;&nbsp;<a href='".$channel."-page-".($page+1).".html'>下一页</a>&nbsp;&nbsp;<a href='".$channel."-page-".$pagecount.".html'>末页</a>";
						}
					}
					$tipage.="&nbsp;&nbsp;(当前".$page."页/共".$pagecount."页)&nbsp;&nbsp;";
				}
			
			}
		
		}

	}
	$multipage.=$tipage;
	return $multipage;
}

?>