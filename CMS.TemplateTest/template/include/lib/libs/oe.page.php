<?php
/*-----------------------------------
 * @version: OEcms v2.0 Free(��Ѱ�)
 * @website: www.phpoe.com,www.phpcoo.com
 * @email  : phpzac@foxmail.com
 * @qq     : 944811833,16100225
 * @update : 2011.02.20
-----------------------------------*/

/**
	ǰ�����·�ҳ�� ��ʽ ��ҳ ��һҳ ��һҳ ĩҳ
	@param $cateid -- ��ǰ����ID
	@param $pagecount -- ��ҳ����
	@param $page -- ��ǰҳ
	@param $pagesize -- ÿҳ��ʾ����
	@param $mpurl -- URL��ַ
*/
function article_showpage($cateid,$pagecount,$page,$pagesize,$mpurl){   
    $multipage ="";
	$tipage = "";
    $mpurl .= strpos($mpurl, '?') ? '&amp;' : '?';
	if($pagecount>1){
		if($page==1 || $page<1){
			$tipage = "��ҳ&nbsp;&nbsp;��һҳ&nbsp;&nbsp;<a href='".$mpurl."page=".($page+1)."'>��һҳ</a>&nbsp;&nbsp;<a href='".$mpurl."page=".$pagecount."'>ĩҳ</a>";
		}else{
			if($page == $pagecount){
				$tipage = "<a href='".$mpurl."page=1'>��ҳ</a>&nbsp;&nbsp;<a href='".$mpurl."page=".($page-1)."'>��һҳ</a>&nbsp;&nbsp;��һҳ&nbsp;&nbsp;ĩҳ";
			}else{
				$tipage = "<a href='".$mpurl."page=1'>��ҳ</a>&nbsp;&nbsp;<a href='".$mpurl."page=".($page-1)."'>��һҳ</a>&nbsp;&nbsp;<a href='".$mpurl."page=".($page+1)."'>��һҳ</a>&nbsp;&nbsp;<a href='".$mpurl."page=".$pagecount."'>ĩҳ</a>";
			}
		}
		$tipage.="&nbsp;&nbsp;(��ǰ".$page."ҳ/��".$pagecount."ҳ)&nbsp;&nbsp;";
	}
	$multipage.=$tipage;
	return $multipage;
}

/**
	ǰ�����·�ҳ�� ��ʽ ��ҳ ��һҳ ��һҳ ĩҳ α��̬
	@param $channel   -- Ƶ��(����Rewrite URL)
	@param $cateid    -- ��ǰ����ID
	@param $keyword   -- ��Ʒ�ؼ���
	@param $pagecount -- ��ҳ����
	@param $page      -- ��ǰҳ
	@param $pagesize  -- ÿҳ��ʾ����
	@param $mpurl     -- URL��ַ(��Rewrite URL)
*/
function url_showpage($channel,$cateid,$keyword,$pagecount,$page,$pagesize,$mpurl){   
    $multipage ="";
	$tipage = "";

    //URL·�ɴ���
	if($GLOBALS['config']['htmltype']=="php"){
        $mpurl .= strpos($mpurl, '?') ? '&amp;' : '?';
		if($pagecount>1){
			if($page==1 || $page<1){
				$tipage = "��ҳ&nbsp;&nbsp;��һҳ&nbsp;&nbsp;<a href='".$mpurl."page=".($page+1)."'>��һҳ</a>&nbsp;&nbsp;<a href='".$mpurl."page=".$pagecount."'>ĩҳ</a>";
			}else{
				if($page == $pagecount){
					$tipage = "<a href='".$mpurl."page=1'>��ҳ</a>&nbsp;&nbsp;<a href='".$mpurl."page=".($page-1)."'>��һҳ</a>&nbsp;&nbsp;��һҳ&nbsp;&nbsp;ĩҳ";
				}else{
					$tipage = "<a href='".$mpurl."page=1'>��ҳ</a>&nbsp;&nbsp;<a href='".$mpurl."page=".($page-1)."'>��һҳ</a>&nbsp;&nbsp;<a href='".$mpurl."page=".($page+1)."'>��һҳ</a>&nbsp;&nbsp;<a href='".$mpurl."page=".$pagecount."'>ĩҳ</a>";
				}
			}
			$tipage.="&nbsp;&nbsp;(��ǰ".$page."ҳ/��".$pagecount."ҳ)&nbsp;&nbsp;";
		}
	}else{

		//α��̬
		if((int)$pagecount>1){
            
			
			if((int)$cateid>0){
                //�з���ID��
				if($page==1 || $page<1){
					$tipage = "��ҳ&nbsp;&nbsp;��һҳ&nbsp;&nbsp;<a href='".$channel."-cat-".$cateid."-page-".($page+1).".html'>��һҳ</a>&nbsp;&nbsp;<a href='".$channel."-cat-".$cateid."-page-".$pagecount.".html'>ĩҳ</a>";
				}else{
					if($page == $pagecount){
						$tipage = "<a href='".$channel."-cat-".$cateid."-page-1.html'>��ҳ</a>&nbsp;&nbsp;<a href='".$channel."-cat-".$cateid."-page-".($page-1).".html'>��һҳ</a>&nbsp;&nbsp;��һҳ&nbsp;&nbsp;ĩҳ";
					}else{
						$tipage = "<a href='".$channel."-cat-".$cateid."-page-1.html'>��ҳ</a>&nbsp;&nbsp;<a href='".$channel."-cat-".$cateid."-page-".($page-1).".html'>��һҳ</a>&nbsp;&nbsp;<a href='".$channel."-cat-".$cateid."-page-".($page+1).".html'>��һҳ</a>&nbsp;&nbsp;<a href='".$channel."-cat-".$cateid."-page-".$pagecount.".html'>ĩҳ</a>";
					}
				}
				$tipage.="&nbsp;&nbsp;(��ǰ".$page."ҳ/��".$pagecount."ҳ)&nbsp;&nbsp;";
			
			}else{
				//û�з���ID��
				if(ischar($keyword)){
					//�ؼ��ִ���
					if($page==1 || $page<1){
						$tipage = "��ҳ&nbsp;&nbsp;��һҳ&nbsp;&nbsp;<a href='".$channel."-k-".urlencode($keyword)."-page-".($page+1).".html'>��һҳ</a>&nbsp;&nbsp;<a href='".$channel."-k-".urlencode($keyword)."-page-".$pagecount.".html'>ĩҳ</a>";
					}else{
						if($page == $pagecount){
							$tipage = "<a href='".$channel."-k-".urlencode($keyword)."-page-1.html'>��ҳ</a>&nbsp;&nbsp;<a href='".$channel."-k-".urlencode($keyword)."-page-".($page-1).".html'>��һҳ</a>&nbsp;&nbsp;��һҳ&nbsp;&nbsp;ĩҳ";
						}else{
							$tipage = "<a href='".$channel."-k-".urlencode($keyword)."-page-1.html'>��ҳ</a>&nbsp;&nbsp;<a href='".$channel."-k-".urlencode($keyword)."-page-".($page-1).".html'>��һҳ</a>&nbsp;&nbsp;<a href='".$channel."-k-".urlencode($keyword)."-page-".($page+1).".html'>��һҳ</a>&nbsp;&nbsp;<a href='".$channel."-k-".urlencode($keyword)."-page-".$pagecount.".html'>ĩҳ</a>";
						}
					}
					$tipage.="&nbsp;&nbsp;(��ǰ".$page."ҳ/��".$pagecount."ҳ)&nbsp;&nbsp;";

				}else{

					//�ؼ���Ϊ��
					if($page==1 || $page<1){
						$tipage = "��ҳ&nbsp;&nbsp;��һҳ&nbsp;&nbsp;<a href='".$channel."-page-".($page+1).".html'>��һҳ</a>&nbsp;&nbsp;<a href='".$channel."-page-".$pagecount.".html'>ĩҳ</a>";
					}else{
						if($page == $pagecount){
							$tipage = "<a href='".$channel."-page-1.html'>��ҳ</a>&nbsp;&nbsp;<a href='".$channel."-page-".($page-1).".html'>��һҳ</a>&nbsp;&nbsp;��һҳ&nbsp;&nbsp;ĩҳ";
						}else{
							$tipage = "<a href='".$channel."-page-1.html'>��ҳ</a>&nbsp;&nbsp;<a href='".$channel."-page-".($page-1).".html'>��һҳ</a>&nbsp;&nbsp;<a href='".$channel."-page-".($page+1).".html'>��һҳ</a>&nbsp;&nbsp;<a href='".$channel."-page-".$pagecount.".html'>ĩҳ</a>";
						}
					}
					$tipage.="&nbsp;&nbsp;(��ǰ".$page."ҳ/��".$pagecount."ҳ)&nbsp;&nbsp;";
				}
			
			}
		
		}

	}
	$multipage.=$tipage;
	return $multipage;
}

?>